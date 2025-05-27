using Application.DTOs.Common;
using Application.DTOs.Payment;
using Application.IApplicationServices.Payment;
using Application.IReositosy;
using AutoMapper;
using Domain.Entities.ApplicationEntities;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ApplicationServices
{
    public class PaymentService : IPaymentService
    {
         private readonly IAppRepository<Payment> _Repo;
        private readonly IMapper _mapper;

        public PaymentService(IAppRepository<Payment> payment ,IMapper mapper)
        {  _mapper = mapper; 

            _Repo=payment;

        }

        public async Task<PaymentDto> CreatePaymentAsync(CreatePaymentDto createPaymentDto)
        {
            var p = _mapper.Map<Payment>(createPaymentDto);
            await _Repo.InsertAsync(p);
            return _mapper.Map<PaymentDto>(p);
            
        }

        public async Task<PaymentDto> DeletePaymentAsync(BaseDto<int> dto)
        {
            var p = (await _Repo.FindAsync(x=> x.Id == dto.Id)).FirstOrDefault();

            await _Repo.RemoveAsync(p);

            return _mapper.Map<PaymentDto>(p);
        }

        public async Task<PaymentDto> GetPaymentByIdAsync(BaseDto<int> dto)
        {
            var p = (await _Repo.FindAsync(x => x.Id == dto.Id)).FirstOrDefault();

            return _mapper.Map<PaymentDto>(p);
        }

        public async Task<IEnumerable<PaymentDto>> GetPaymentsAsync()
        {
            var p = await _Repo.GetAllWithAllIncludeAsync();
            return _mapper.Map<IEnumerable<PaymentDto>>(p);
        }

        public async Task<PaymentDto> UpdatePayment(UpdatePaymentDto updatePaymentDto)
        {
            var p = _mapper.Map<Payment>(updatePaymentDto);
            await _Repo.UpdateAsync(p);
            return _mapper.Map<PaymentDto>(p);
        }
    }
}
