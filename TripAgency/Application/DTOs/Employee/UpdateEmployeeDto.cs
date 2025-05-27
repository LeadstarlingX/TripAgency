using Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Employee
{
    public class UpdateEmployeeDto : BaseDto<long>
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
        public DateTime? HireDate { get; set; }
    }
}