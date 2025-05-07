using Application.DTOs.Authentication;
using Application.IApplicationServices.Authentication;
using Application.IApplicationServices.Customer;
using Application.IReositosy;
using Domain.Common;
using Domain.Entities.ApplicationEntities;
using Domain.Entities.IdentityEntities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ApplicationServices.Authentication
{
    public class AuthenticationService : Application.IApplicationServices.Authentication.IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IIdentityAppRepository<ApplicationUser> _userRepository;
        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor _httpContextAccessor;
        //private readonly ICustomerService _customerService;


        public AuthenticationService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IHttpContextAccessor httpContextAccessor,
            IConfiguration config,
            IIdentityAppRepository<ApplicationUser> identityAppRepository
            //ICustomerService customerService
            )
        {
            _userManager = userManager;
            //_customerService = customerService;
            _signInManager = signInManager;
            _config = config;
            _userRepository = identityAppRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<UserProfileDto> GetAuthenticatedUser()
        {
            if (_httpContextAccessor.HttpContext is null)
                throw new Exception("User Not found");

            var authenticateResult = await _httpContextAccessor.HttpContext.AuthenticateAsync(JwtBearerDefaults.AuthenticationScheme);
            if (!authenticateResult.Succeeded)
                throw new Exception("User Not found");

            ApplicationUser? customer = null;
            var identifierClaim = authenticateResult.Principal.FindFirst(claim => claim.Type == ClaimTypes.NameIdentifier);

            if (identifierClaim != null && int.TryParse(identifierClaim.Value, out int customerId))
            {
                customer = await _userManager.FindByIdAsync(customerId.ToString());                
            }

            if (customer is null)
            {
                throw new Exception("User Not found");                
            }

            if (!customer.IsActive)
            {
                throw new Exception("Deactivated User");                
            }

            var jwtToken = await GenerateJwtToken(customer);
            return new UserProfileDto
            {
                Id = customer.Id,
                Name = customer.UserName ?? string.Empty,
                Email = customer.Email ?? string.Empty,
                Token = jwtToken,
            };
        }
        

        public async Task<UserProfileDto> LoginAsync(LoginDto loginDto)
        {
            var existingUser = await _userManager.FindByEmailAsync(loginDto.Email);
            if (existingUser == null)
                   throw new Exception("email or password not correct");

            var isCorrect = await _userManager.CheckPasswordAsync(existingUser, loginDto.Password);
            if (!isCorrect)
                throw new Exception("email or password not correct");

            if (!existingUser.IsActive) 
                throw new Exception("Deactivated User");
             
            await _signInManager.SignInAsync(existingUser,false);

            var jwtToken = await GenerateJwtToken(existingUser);
            return new UserProfileDto
            {
                Id = existingUser.Id,
                Name = existingUser.UserName ?? string.Empty,
                Email = existingUser.Email ?? string.Empty,
                Token = jwtToken,
            };            
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<IdentityResult> RegisterAsync(RegisterDto dto, bool fromAdmin = false)
        {
            ApplicationUser user = new ApplicationUser
                {
                    UserName = dto.FirstName+dto.LastName,
                    Email = dto.Email,
                    Name = $"{dto.FirstName} {dto.LastName}",
                    Address = dto.Address
                };

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user,(dto.Role).ToString());
            }

            return result;
        }

        private async Task<TokenDto> GenerateJwtToken(ApplicationUser user)
        {
            try
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var jwtTokenHandler = new JwtSecurityTokenHandler();
                var secret = _config["Jwt:Secret"]??string.Empty;
                var key = Encoding.ASCII.GetBytes(secret);
                var claims = new List<Claim>
                {
                    new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new(ClaimTypes.Name, user.Name ?? string.Empty),
                    new(ClaimTypes.Email, user.Email ?? string.Empty),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                foreach (var role in userRoles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Audience = _config["Jwt:Audience"],
                    Issuer = _config["Jwt:Issuer"],
                    Subject = new ClaimsIdentity(claims.ToArray()),
                    Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_config["Jwt:ExpiryInMinutes"])),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = jwtTokenHandler.CreateToken(tokenDescriptor);
                var jwtToken = jwtTokenHandler.WriteToken(token);

                return new TokenDto()
                {
                    Token = jwtToken,
                    Success = true,
                    UserRoles = userRoles,
                };
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
