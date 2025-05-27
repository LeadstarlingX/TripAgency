using Application.Common;
using Application.DTOs.Authentication;
using Application.DTOs.Contact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Customer
{
    public class CreateCustomerDto : BaseDto<long>
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string Email {  get; set; } = string.Empty;  
        public required RegisterDto UserDto { get; set; }
    }
}