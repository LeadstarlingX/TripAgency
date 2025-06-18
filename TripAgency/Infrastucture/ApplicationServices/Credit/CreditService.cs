using Application.DTOs.Credit;
using Application.DTOs.Customer;
using Application.DTOs.PaymentMethod;
using Domain.Entities.ApplicationEntities;
using Application.IReositosy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.IApplicationServices.Credit;
using Infrastructure.Extension;
using AutoMapper;
using Application.Common;

namespace Infrastructure.ApplicationServices.Credit
{
    public class CreditService : ICreditService
    {
        private readonly IAppRepository<Domain.Entities.ApplicationEntities.Credit> _creditRepository;
        private readonly IAppRepository<Domain.Entities.ApplicationEntities.Customer> _customerRepository;
        private readonly IAppRepository<Domain.Entities.ApplicationEntities.PaymentMethod> _paymentMethodRepository;
        private readonly IMapper _mapper;

        public CreditService(
            IAppRepository<Domain.Entities.ApplicationEntities.Credit> creditRepository,
            IAppRepository<Domain.Entities.ApplicationEntities.Customer> customerRepository,
            IAppRepository<Domain.Entities.ApplicationEntities.PaymentMethod> paymentMethodRepository,
            IMapper mapper)
        {
            _creditRepository = creditRepository;
            _customerRepository = customerRepository;
            _paymentMethodRepository = paymentMethodRepository;
            _mapper = mapper;
        }

        public async Task<CreditsDto> GetCreditsAsync()
        {
            var credits = await _creditRepository.GetAllAsync(false, c => c.Customer!, c => c.PaymentMethod!);
            return new CreditsDto
            {
                Credits = credits.Select(c => _mapper.Map<CreditDto>(c)).ToList()
            };
        }

        public async Task<CreditDto> GetCreditByIdAsync(BaseDto<long> dto)
        {
            var credit = (await _creditRepository.FindAsync(c => c.Id == dto.Id, false, c => c.Customer!, c => c.PaymentMethod!)).FirstOrDefault()
                ?? throw new KeyNotFoundException("Credit not found");

            return _mapper.Map<CreditDto>(credit);
        }

        public async Task<CreditDto> CreateCreditAsync(CreateCreditDto dto)
        {
            // Validate customer exists
            var customer = (await _customerRepository.FindAsync(c => c.UserId == dto.CustomerId)).FirstOrDefault()
                ?? throw new KeyNotFoundException("Customer not found");

            // Validate payment method exists
            var paymentMethod = (await _paymentMethodRepository.FindAsync(pm => pm.Id == dto.PaymentMethodId)).FirstOrDefault()
                ?? throw new KeyNotFoundException("Payment method not found");

            var credit = _mapper.Map<Domain.Entities.ApplicationEntities.Credit>(dto);
            var created = await _creditRepository.InsertAsync(credit);
            
            // Reload with navigation properties
            var createdWithNav = await _creditRepository.FindAsync(c => c.Id == created.Id, false, c => c.Customer!, c => c.PaymentMethod!);
            return _mapper.Map<CreditDto>(createdWithNav.FirstOrDefault());
        }

        public async Task<CreditDto> UpdateCreditAsync(UpdateCreditDto dto)
        {
            var existingCredit = (await _creditRepository.FindAsync(c => c.Id == dto.Id)).FirstOrDefault()
                ?? throw new KeyNotFoundException("Credit not found");

            // Only update fields that have values (not null)
            if (dto.CreditAmount.HasValue)
            {
                existingCredit.CreditAmount = dto.CreditAmount.Value;
            }
            
            if (dto.IsActive.HasValue)
            {
                existingCredit.IsActive = dto.IsActive.Value;
            }

            var updated = await _creditRepository.UpdateAsync(existingCredit);
            
            // Reload with navigation properties
            var updatedWithNav = await _creditRepository.FindAsync(c => c.Id == updated.Id, false, c => c.Customer, c => c.PaymentMethod);
            return _mapper.Map<CreditDto>(updatedWithNav.FirstOrDefault());
        }

        public async Task DeleteCreditAsync(BaseDto<long> dto)
        {
            var credit = (await _creditRepository.FindAsync(c => c.Id == dto.Id)).FirstOrDefault()
                ?? throw new KeyNotFoundException("Credit not found");

            await _creditRepository.RemoveAsync(credit);
        }

        public async Task<CreditsDto> GetCreditsByCustomerAsync(BaseDto<long> customerDto)
        {
            var credits = await _creditRepository.FindAsync(c => c.CustomerId == customerDto.Id, false, c => c.Customer, c => c.PaymentMethod);
            return new CreditsDto
            {
                Credits = credits.Select(c => _mapper.Map<CreditDto>(c)).ToList()
            };
        }

        public async Task<CreditsDto> GetCreditsByPaymentMethodAsync(BaseDto<int> paymentMethodDto)
        {
            var credits = await _creditRepository.FindAsync(c => c.PaymentMethodId == paymentMethodDto.Id, false, c => c.Customer, c => c.PaymentMethod);
            return new CreditsDto
            {
                Credits = credits.Select(c => _mapper.Map<CreditDto>(c)).ToList()
            };
        }
    }
} 