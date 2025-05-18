using Domain.Context;
using Domain.Enum;
using Domain.Common;
using Domain.Entities.ApplicationEntities;
using Domain.Entities.IdentityEntities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Infrastructure.Seeds
{
    public class DataSeeder(
        ApplicationDbContext context,
        RoleManager<ApplicationRole> roleManeger,
        IdentityAppDbContext identityAppDbContext,
        UserManager<ApplicationUser> userManager)
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IdentityAppDbContext _identityAppDbContext = identityAppDbContext;
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly RoleManager<ApplicationRole> _roleManeger = roleManeger;
        public void SeedData()
        {
            var shouldUpdateContext = false;


            if (shouldUpdateContext)
            {
                shouldUpdateContext = false;
                _context.SaveChanges();
            }
            if(!_identityAppDbContext.Roles.Any())
            {
                shouldUpdateContext = true;

                var roleUser = new ApplicationRole()
                {
                    Name = DefaultSetting.UserRoleName,
                };
                var roleEmployee = new ApplicationRole()
                {
                    Name = DefaultSetting.EmployeeRoleName,
                };
                var roleAdmin = new ApplicationRole()
                {
                    Name = DefaultSetting.AdminRoleName
                };
                var roleCustomer = new ApplicationRole()
                {
                    Name = DefaultSetting.CustomerRoleName
                };
                _roleManeger.CreateAsync(roleAdmin).GetAwaiter().GetResult();
                _roleManeger.CreateAsync(roleUser).GetAwaiter().GetResult();
                _roleManeger.CreateAsync(roleEmployee).GetAwaiter().GetResult();
                _roleManeger.CreateAsync(roleCustomer).GetAwaiter().GetResult();

            }

            if (!_identityAppDbContext.Users.Any(u => u.Email == DefaultSetting.DefaultAdminOneEmail))
            {
                var newUser = new ApplicationUser()
                {
                    Email = DefaultSetting.DefaultAdminOneEmail,
                    UserName = DefaultSetting.AdminRoleName,
                    PhoneNumber = DefaultSetting.DefaultAdminOnePhone,
                    PhoneNumberConfirmed = true,
                    IsActive = true
                };

                var isCreated = _userManager.CreateAsync(newUser, DefaultSetting.DefaultAdminPassword).GetAwaiter().GetResult();
                if (isCreated.Succeeded)
                {
                    _userManager.AddToRoleAsync(newUser, DefaultSetting.AdminRoleName).GetAwaiter().GetResult();
                    var code = _userManager.GenerateEmailConfirmationTokenAsync(newUser).GetAwaiter().GetResult();
                    _userManager.ConfirmEmailAsync(newUser, code).GetAwaiter().GetResult();
                }
                shouldUpdateContext = true;

            }


            if (shouldUpdateContext)
            {
                shouldUpdateContext = false;
                _context.SaveChanges();
            }
        }
    }

}
