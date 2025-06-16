using Application.Common;
using Application.DTOs.Contact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Customer
{
    public class UpdateCustomerContactDto : BaseDto<int>
    {
        public string? Value { get; set; }
    }
}
