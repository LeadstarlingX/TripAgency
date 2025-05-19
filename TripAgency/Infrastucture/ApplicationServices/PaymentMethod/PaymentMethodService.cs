using Application.DTOs.Car;
using Application.DTOs.Common;
using Application.DTOs.PaymentMethod;
using Application.IApplicationServices;
using Application.IReositosy;
using AutoMapper;
using Domain.Entities.ApplicationEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ApplicationServices
{
    public class PaymentMethodService : IPaymentMethodService
    {


        private readonly IAppRepository<PaymentMethod> _paymentMetodrepo;
        private readonly IMapper _mapper;


        public PaymentMethodService(IAppRepository<PaymentMethod>paymentMethod ,IMapper mapper)
        { 
          _paymentMetodrepo = paymentMethod;
            _mapper = mapper;
        }


        public async Task<PaymentMethodDto> CreatePaymentMethodAsync(CreatePaymentMethodDto createPaymentMethodDto)
        {
            var P = _mapper.Map<PaymentMethod>(createPaymentMethodDto);
             await _paymentMetodrepo.InsertAsync(_mapper.Map<PaymentMethod>(createPaymentMethodDto));

            return _mapper.Map<PaymentMethodDto>(P);
        }

        public async Task<PaymentMethodDto> DeletePaymentMethodAsync(BaseDto<int> dto)
        {

            var p =await _paymentMetodrepo.FindAsync(x=>x.Id == dto.Id);

            await _paymentMetodrepo.RemoveAsync(_mapper.Map<PaymentMethod>(dto));
            return _mapper.Map<PaymentMethodDto>(p);
        }

        public async Task<IEnumerable<PaymentMethodDto>> GetPaymentMethodsAsync()
        {
            var result =await _paymentMetodrepo.GetAllWithAllIncludeAsync();

            return _mapper.Map<IEnumerable<PaymentMethodDto>>(result);
        }

        public async Task<PaymentMethodDto> UpdatePaymentMethod(UpdatePaymentMethodDto updatePaymentMethodDto)
        {
            var p = _mapper.Map<PaymentMethod>(updatePaymentMethodDto);

            await _paymentMetodrepo.UpdateAsync(_mapper.Map<PaymentMethod>(p));
            return _mapper.Map<PaymentMethodDto>(p);

        }

      public async Task<PaymentMethodDto> GetPaymentMethodByIdAsync(BaseDto<int> dto)
        {
            var result =(await _paymentMetodrepo.FindAsync(x=>x.Id==dto.Id)).FirstOrDefault();

            return _mapper.Map<PaymentMethodDto>(result);
        }

        
    }
}
