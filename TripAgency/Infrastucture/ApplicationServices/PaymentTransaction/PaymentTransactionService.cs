using Application.DTOs;
using Application.DTOs.Common;
using Application.DTOs.PaymentTransaction;
using Application.DTOs.PaymentTransactionDto;
using Application.IApplicationServices.PaymentTransaction;
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
    public class PaymentTransactionService : IPaymentTransactionService
    {

        private readonly IAppRepository<PaymentTransaction> _paymenttransaction;

        private readonly IMapper _mapper;
        public PaymentTransactionService(IMapper mapper ,IAppRepository<PaymentTransaction>trans)
        {
            _mapper = mapper;
            _paymenttransaction = trans;
        }

        public async Task<PaymentTransactionDto> CreatePaymentTransactionAsync(CreatePaymentTransactionDto createPaymentTranDto)
        {
           var t = _mapper.Map<PaymentTransaction>(createPaymentTranDto);
            await _paymenttransaction.InsertAsync(t);
            return _mapper.Map<PaymentTransactionDto>(t);
        }

        public async Task<PaymentTransactionDto> DeletePaymentTransactionAsync(BaseDto<int> dto)
        {
            var t = (await _paymenttransaction.FindAsync(x=>x.Id == dto.Id)).FirstOrDefault();
            var s= _mapper.Map<PaymentTransaction>(t);
            await _paymenttransaction.RemoveAsync(s);

            return _mapper.Map<PaymentTransactionDto>(t);
           
        }

        public async Task<IEnumerable<PaymentTransactionDto>> GetPaymentTransactionsAsync() =>

           _mapper.Map<IEnumerable<PaymentTransactionDto>>(await _paymenttransaction.GetAllWithAllIncludeAsync());
        

        public async Task<PaymentTransactionDto> GetPaymentTransactionByIdAsync(BaseDto<int> dto)
        {
            var t = (await _paymenttransaction.FindAsync(x=>x.Id==dto.Id)).FirstOrDefault();

            return _mapper.Map<PaymentTransactionDto>(t);
        }

        public async Task<PaymentTransactionDto> UpdatePaymentTransaction(UpdatePaymentTransactionDto updatePaymentTranDto)
        {
            var t = _mapper.Map<PaymentTransaction>(updatePaymentTranDto);

            await _paymenttransaction.UpdateAsync(t);

            return _mapper.Map<PaymentTransactionDto>(t);
        }
    }
}
