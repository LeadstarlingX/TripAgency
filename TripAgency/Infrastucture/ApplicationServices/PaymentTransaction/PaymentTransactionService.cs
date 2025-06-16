using Application.Common;
using Application.DTOs;
using Application.DTOs.Payment;
using Application.DTOs.PaymentTransaction;
using Application.IApplicationServices.Payment;
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

        private readonly IPaymentService _service;
        public PaymentTransactionService(IMapper mapper ,IAppRepository<PaymentTransaction>trans ,IPaymentService paymentService)
        {
            _mapper = mapper;
            _paymenttransaction = trans;
            _service = paymentService;
        }

        public async Task<PaymentTransactionDto> CreatePaymentTransactionAsync(CreatePaymentTransactionDto createPaymentTranDto)
        {
           var t = _mapper.Map<PaymentTransaction>(createPaymentTranDto);
            await _paymenttransaction.InsertAsync(t);
            BaseDto<int> b = new BaseDto<int> { Id = createPaymentTranDto.PaymentId };
            PaymentDto paymentDto = await _service.GetPaymentByIdAsync(b);

            UpdatePaymentDto p;

            p = new UpdatePaymentDto
            {
                Id = createPaymentTranDto.PaymentId,
              
            };

           // p.Id = createPaymentTranDto.PaymentId;
            p.BookingId = paymentDto.BookingId;
           
            p.Notes =   $"transaction{createPaymentTranDto.PaymentId}";
            p.AmountDue -= createPaymentTranDto.Amount;
            p.AmountPaid += createPaymentTranDto.Amount;
            if (createPaymentTranDto.TransactionType == Domain.Enum.TransactionTypeEnum.Deposit)
            {
                p.Status = Domain.Enum.PaymentStatusEnum.Pending;
            }
            else if (p.Status == Domain.Enum.PaymentStatusEnum.complete)
            {
                p.Status = Domain.Enum.PaymentStatusEnum.complete;
            }
            else
            {
                p.Status = Domain.Enum.PaymentStatusEnum.refund;
            }
            if(p.Status== Domain.Enum.PaymentStatusEnum.complete)
            {
                p.PaymentDate = DateTime.Now;
            }

            await _service.UpdatePayment(p);
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

            public  async Task<IEnumerable<PaymentTransactionDto>> GetPaymentTransactionForPayment(BaseDto<int> dto)
        {
            var p = await _paymenttransaction.FindAsync(x => x.PaymentId == dto.Id, x => x.Payment);
            return _mapper.Map<IEnumerable<PaymentTransactionDto>>(p);

        }

         public async Task<IEnumerable<PaymentTransactionDto>> GetPaymentTransactionDtosByMethod(BaseDto<int> dto)
        {
            var t = await _paymenttransaction.FindAsync(x => x.PaymentMethodId == dto.Id, p => p.PaymentMethod);

            return _mapper.Map<IEnumerable<PaymentTransactionDto>>(t);
        }
    }
}
