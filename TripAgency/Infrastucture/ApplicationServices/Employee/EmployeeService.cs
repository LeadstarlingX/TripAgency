using Application.DTOs.Common;
using Application.DTOs.Employee;
using Application.IApplicationServices.Employee;
using Application.IReositosy;
using AutoMapper;
using Domain.Entities.IdentityEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ApplicationServices.Employee
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IAppRepository<Domain.Entities.ApplicationEntities.Employee> _employeeRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public EmployeeService(
            IAppRepository<Domain.Entities.ApplicationEntities.Employee> employeeRepository,
            UserManager<ApplicationUser> userManager,
            IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<EmployeesDto> GetEmployeesAsync()
        {
            var employees = await _employeeRepository.GetAllAsync();
            return new EmployeesDto
            {
                Employees = employees.Select(e => _mapper.Map<EmployeeDto>(e)).ToList()
            };
        }

        public async Task<EmployeeDto> CreateEmployeeAsync(CreateEmployeeDto dto)
        {
            var user = new ApplicationUser
            {
                UserName = dto.UserDto.FirstName + dto.UserDto.LastName,
                Email = dto.UserDto.Email,
                Name = dto.Name,
                Address = dto.Address,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                TwoFactorEnabled = false,
                LockoutEnabled = true,
                AccessFailedCount = 0
            };

            var result = await _userManager.CreateAsync(user, dto.UserDto.Password);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception($"User creation failed: {errors}");
            }

            var employee = _mapper.Map<Domain.Entities.ApplicationEntities.Employee>(dto);
            employee.UserId = user.Id;

            var created = await _employeeRepository.InsertAsync(employee);
            return _mapper.Map<EmployeeDto>(created);
        }

        public async Task<EmployeeDto> UpdateEmployeeAsync(UpdateEmployeeDto dto)
        {
            var employee = (await _employeeRepository.FindAsync(e => e.UserId == dto.Id))
                .FirstOrDefault() ?? throw new KeyNotFoundException("Employee not found");

            _mapper.Map(dto, employee);
            var updated = await _employeeRepository.UpdateAsync(employee);
            return _mapper.Map<EmployeeDto>(updated);
        }

        public async Task DeleteEmployeeAsync(BaseDto<long> dto)
        {
            var employee = (await _employeeRepository.FindAsync(e => e.UserId == dto.Id))
                .FirstOrDefault() ?? throw new KeyNotFoundException("Employee not found");

            await _employeeRepository.RemoveAsync(employee);
        }
    }
}
