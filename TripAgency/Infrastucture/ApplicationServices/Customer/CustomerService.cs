using Application.DTOs.Common;
using Application.DTOs.Contact;
using Application.DTOs.Customer;
using Domain.Entities.ApplicationEntities;
using Application.IReositosy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.IApplicationServices.Authentication;
using Microsoft.AspNetCore.Identity;
using Application.IApplicationServices.Customer;
using Domain.Entities.IdentityEntities;
using Application.IApplicationServices.Contact;
using Infrastructure.Extension;

namespace Infrastructure.ApplicationServices.Customer
{
    public class CustomerService : ICustomerService
    {
        private readonly IAppRepository<Domain.Entities.ApplicationEntities.Customer> _customerRepository;
        private readonly IAppRepository<CustomerContact> _customerContactRepository;
        private readonly IAuthenticationService _authenticationService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IContactTypeService _contactTypeService;

        public CustomerService(
            IAppRepository<Domain.Entities.ApplicationEntities.Customer> customerRepository,
            IAppRepository<CustomerContact> customerContactRepository,
            IAuthenticationService authenticationService,
            UserManager<ApplicationUser> userManager,
             IContactTypeService contactTypeService)
        {
            _customerRepository = customerRepository;
            _customerContactRepository = customerContactRepository;
            _authenticationService = authenticationService;
            _userManager = userManager;
            _contactTypeService = contactTypeService;
        }

        public async Task<CustomersDto> GetCustomersAsync()
        {
            var customers = await _customerRepository.GetAllAsync(c => c.Contacts);
            return new CustomersDto
            {
                Customers = customers.Select(c => MapToDto(c)).ToList()
            };
        }

        public async Task<CustomerDto> CreateCustomerAsync(CreateCustomerDto dto)
        {
            var customerAsUser = await _userManager.FindByEmailAsync(dto.Email)??throw new Exception("User not found");
            var customer = new Domain.Entities.ApplicationEntities.Customer()
            {
                UserId = customerAsUser.Id,
                FirstName = dto.FirstName,
                Country = dto.Country,
            };

            if (dto.Contacts is not null && !dto.Contacts.Contacts.Any())
            {
                customer.Contacts = dto.Contacts.Contacts.Select( c => new CustomerContact
                {
                    CustomerId = customerAsUser.Id,
                    Value = c.Value,
                    ContactTypeId = c.Id,
                }).ToList();
            }
            customer.Contacts.Add(new CustomerContact()
            {
                CustomerId = customerAsUser.Id,
                Value = dto.Email,
                ContactTypeId = (_contactTypeService.GetContactByTypeAsync(Domain.Enum.ContactTypeEnum.Email).GetAwaiter().GetResult()).Id,
            });

            var created = await _customerRepository.InsertAsync(customer);
            return MapToDto(created);
        }

        public async Task<CustomerDto> UpdateCustomerAsync(UpdateCustomerDto dto)
        {
            var customer = (await _customerRepository.FindAsync(c => c.UserId == dto.Id)).FirstOrDefault();
            if (customer == null) throw new KeyNotFoundException("Customer not found");

            

            await _customerRepository.UpdateAsync(customer);
            return MapToDto(customer);
        }

        public async Task DeleteCustomerAsync(BaseDto<long> dto)
        {
            var customer = (await _customerRepository.FindAsync(c => c.UserId == dto.Id)).FirstOrDefault();
            if (customer == null) throw new KeyNotFoundException("Customer not found");

            //customer.IsDeleted = true;
            await _customerRepository.RemoveAsync(customer);
        }

        public async Task<ContactsDto> GetCustomerContactAsync()
        {
            var currentUser = await _authenticationService.GetAuthenticatedUser();
            var customer = (await _customerContactRepository.FindAsync(
                c => c.CustomerId == currentUser.Id,
                c => c.ContactType!));

            if (customer == null) throw new KeyNotFoundException("Customer not found");

            return new ContactsDto
            {
                Contacts = customer.Select(c => new ContactDto
                {
                    Id = c.Id,
                    Type = c.ContactType!.Type,
                    Value = c.Value
                }).ToList()
            };
        }

        public async Task<CustomerDto> UpdateCustomerContactsAsync()
        {
            throw new NotImplementedException("Contact data required for update");
        }

        public async Task<CustomerDto> DeleteCustomerContactAsync()
        {
            throw new NotImplementedException("Contact ID required for deletion");
        }

        private CustomerDto MapToDto(Domain.Entities.ApplicationEntities.Customer customer)
        {
            return new CustomerDto
            {
                Id = customer.UserId,
                Name = customer.FirstName + customer.LastName,
                Country = customer.Country,
                Contacts = new ContactsDto()
                {
                    Contacts = customer.Contacts.Select(c => new ContactDto
                    {
                        Id = c.Id,
                        Value = c.Value,
                        Type = c.ContactType!.Type                        
                    }).ToList()
                }
            };
        }
    }
}