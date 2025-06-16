using Application.Common;
using Application.DTOs.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IApplicationServices.Employee
{
    public interface IEmployeeService
    {
        Task<EmployeesDto> GetEmployeesAsync();
        Task<EmployeeDto> CreateEmployeeAsync(CreateEmployeeDto dto);
        Task<EmployeeDto> UpdateEmployeeAsync(UpdateEmployeeDto dto);
        Task DeleteEmployeeAsync(BaseDto<long> dto);
    }
}