using Application.Common;
using Application.DTOs.Contact;
using Domain.Entities.ApplicationEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Customer
{
    public class CustomerDto : BaseDto<long>
    {
        public string Name { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public required ContactsDto Contacts { get; set; }
    }

    public class CustomersDto
    {
        public IList<CustomerDto> Customers { get; set; } = [];
    }
}