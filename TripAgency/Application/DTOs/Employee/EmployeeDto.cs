using Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Employee
{
    public class EmployeeDto : BaseDto<long>
    {
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public DateTime HireDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class EmployeesDto
    {
        public IList<EmployeeDto> Employees { get; set; } = [];
    }
}
