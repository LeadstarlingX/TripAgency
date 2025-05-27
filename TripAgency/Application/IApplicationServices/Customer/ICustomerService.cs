using Application.Common;
using Application.DTOs.Contact;
using Application.DTOs.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IApplicationServices.Customer
{
    public interface ICustomerService
    {
        Task<CustomersDto> GetCustomersAsync();
        Task<CustomerDto> CreateCustomerAsync(CreateCustomerDto createCustomerDto);
        Task<CustomerDto> UpdateCustomerAsync(UpdateCustomerDto updateCustomerDto);
        Task DeleteCustomerAsync(BaseDto<long> dto);
        Task<ContactsDto> GetCustomerContactAsync(BaseDto<long> dto);
        Task CreateCustomerContactAsync(CreateCustomerContactDto createCustomerContactDto);
        Task UpdateCustomerContactAsync(UpdateCustomerContactDto dto);
        Task DeleteCustomerContactAsync(BaseDto<int> customerContatDto);

    }
}