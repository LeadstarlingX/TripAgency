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
using Domain.Enum;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Infrastructure.ApplicationServices.Customer
{
    public class CustomerService : ICustomerService
    {
        private readonly IAppRepository<Domain.Entities.ApplicationEntities.Customer> _customerRepository;
        private readonly IAppRepository<CustomerContact> _customerContactRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IContactTypeService _contactTypeService;
        private readonly IMapper _mapper;

        public CustomerService(
            IAppRepository<Domain.Entities.ApplicationEntities.Customer> customerRepository,
            IAppRepository<CustomerContact> customerContactRepository,
            UserManager<ApplicationUser> userManager,
             IContactTypeService contactTypeService,
             IMapper mapper)
        {
            _customerRepository = customerRepository;
            _customerContactRepository = customerContactRepository;
            _userManager = userManager;
            _contactTypeService = contactTypeService;
            _mapper = mapper;
        }

        public async Task<CustomersDto> GetCustomersAsync()
        {
            var customers = await _customerRepository.GetAllAsync(c => c.Contacts);
            return new CustomersDto
            {
                Customers = customers.Select(c => _mapper.Map<CustomerDto>(c)).ToList()
            };
        }

        public async Task<CustomerDto> CreateCustomerAsync(CreateCustomerDto dto)
        {
            var customer = _mapper.Map<Domain.Entities.ApplicationEntities.Customer>(dto);

            var emailContactType = await _contactTypeService.GetContactByTypeAsync(ContactTypeEnum.Email)
                ?? throw new KeyNotFoundException("Email contact type not found.");

            customer.Contacts.Add(new CustomerContact
            {
                CustomerId = dto.Id,
                Value = dto.Email,
                ContactTypeId = emailContactType.Id
            });
            
            var created = await _customerRepository.InsertAsync(customer);
            return _mapper.Map<CustomerDto>(created);
        }

        public async Task<CustomerDto> UpdateCustomerAsync(UpdateCustomerDto dto)
        {
            var existingCustomer = (await _customerRepository.FindAsync(c => c.UserId == dto.Id)).FirstOrDefault()
                ?? throw new KeyNotFoundException("Customer not found");

            var user = await _userManager.FindByIdAsync(existingCustomer!.UserId.ToString())
                ?? throw new KeyNotFoundException("User not found");

            bool changedName = false;
            if(dto.FirstName is not null && existingCustomer.FirstName != dto.FirstName)
            {
                changedName = true;
                existingCustomer.FirstName = dto.FirstName!;
            }
            if (dto.LastName is not null && existingCustomer.LastName != dto.LastName)
            {
                changedName = true;
                existingCustomer.LastName = dto.LastName!;
            }

            if (dto.Country is not null && existingCustomer.Country != dto.Country)
            {
                existingCustomer.Country = dto.Country!;
                user.Address = dto.Country;
            }
            if(changedName)
                user.UserName = dto.FirstName + dto.LastName; 

            var userResult = await _userManager.UpdateAsync(user);
            if (!userResult.Succeeded)
            {
                var errors = string.Join(", ", userResult.Errors.Select(e => e.Description));
                throw new Exception($"User update failed: {errors}");
            }

            var updatedCustomer = await _customerRepository.UpdateAsync(existingCustomer);
            return _mapper.Map<CustomerDto>(updatedCustomer);
        }

        public async Task DeleteCustomerAsync(BaseDto<long> dto)
        {
            var customer = (await _customerRepository.FindAsync(c => c.UserId == dto.Id)).FirstOrDefault()
                ?? throw new KeyNotFoundException("Customer not found");

            var user = await _userManager.FindByIdAsync(dto!.Id.ToString())
                ?? throw new KeyNotFoundException("User not found");

            var userResult = await _userManager.DeleteAsync(user);
            if (!userResult.Succeeded)
            {
                var errors = string.Join(", ", userResult.Errors.Select(e => e.Description));
                throw new Exception($"User delete failed: {errors}");
            }
            await _customerRepository.RemoveAsync(customer);
        }

        public async Task<ContactsDto> GetCustomerContactAsync(BaseDto<long> dto)
        {
            var customer = (await _customerRepository.FindAsync(c => c.UserId == dto.Id)).FirstOrDefault() ??
                throw new KeyNotFoundException("Customer not found");
            var customerContacts = (await _customerContactRepository.FindAsync(c => c.CustomerId == dto.Id, c => c.ContactType!)) ?? [];

            return new ContactsDto
            {
                Contacts = customerContacts.Select(c => _mapper.Map<ContactDto>(c)).ToList()
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
    }
}