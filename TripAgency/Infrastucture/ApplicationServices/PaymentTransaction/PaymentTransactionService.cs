using Application.Common;
using Application.DTOs;
using Application.DTOs.Payment;
using Application.DTOs.PaymentTransaction;
using Application.IApplicationServices.PaymentTransaction;
using Application.IApplicationServices.Credit;
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
        private readonly IAppRepository<Payment> _paymentRepository;
        private readonly IAppRepository<Booking> _bookingRepository;
        private readonly IAppRepository<Domain.Entities.ApplicationEntities.Credit> _creditRepository;
        private readonly IMapper _mapper;
        private readonly ICreditService _creditService;

        public PaymentTransactionService(
            IMapper mapper, 
            IAppRepository<PaymentTransaction> trans, 
            IAppRepository<Payment> paymentRepository,
            IAppRepository<Booking> bookingRepository,
            IAppRepository<Domain.Entities.ApplicationEntities.Credit> creditRepository,
            ICreditService creditService)
        {
            _mapper = mapper;
            _paymenttransaction = trans;
            _paymentRepository = paymentRepository;
            _bookingRepository = bookingRepository;
            _creditRepository = creditRepository;
            _creditService = creditService;
        }

        public async Task<PaymentTransactionDto> CreatePaymentTransactionAsync(CreatePaymentTransactionDto createPaymentTranDto)
        {
            var t = _mapper.Map<PaymentTransaction>(createPaymentTranDto);
            await _paymenttransaction.InsertAsync(t);
            
            // Get payment directly from repository instead of using service
            var payment = (await _paymentRepository.FindAsync(p => p.Id == createPaymentTranDto.PaymentId)).FirstOrDefault()
                ?? throw new KeyNotFoundException($"Payment with ID {createPaymentTranDto.PaymentId} not found");

            // Handle different transaction types
            if (createPaymentTranDto.TransactionType == Domain.Enum.TransactionTypeEnum.Final)
            {
                await ProcessCreditPayment(createPaymentTranDto, payment);
                payment.AmountPaid += createPaymentTranDto.Amount;
                payment.Status = Domain.Enum.PaymentStatusEnum.complete;
                payment.PaymentDate = DateTime.Now;
            }
            else if (createPaymentTranDto.TransactionType == Domain.Enum.TransactionTypeEnum.Deposit)
            {
                await ProcessCreditPayment(createPaymentTranDto, payment);
                payment.AmountPaid += createPaymentTranDto.Amount;
                payment.Status = Domain.Enum.PaymentStatusEnum.Pending;
            }
            else if (createPaymentTranDto.TransactionType == Domain.Enum.TransactionTypeEnum.Refund)
            {
                payment.AmountPaid -= createPaymentTranDto.Amount;
                payment.Status = Domain.Enum.PaymentStatusEnum.refund;
                payment.PaymentDate = DateTime.Now;
            }

            payment.Notes = $"transaction{createPaymentTranDto.PaymentId}";
            
            // Update payment directly using repository
            await _paymentRepository.UpdateAsync(payment);
            return _mapper.Map<PaymentTransactionDto>(t);
        }

        private async Task ProcessCreditPayment(CreatePaymentTransactionDto transactionDto, Payment payment)
        {
            // Get the booking to find the customer
            var booking = (await _bookingRepository.FindAsync(b => b.Id == payment.BookingId)).FirstOrDefault()
                ?? throw new KeyNotFoundException("Booking not found");

            await _creditService.DeductCreditAsync(booking.CustomerId, transactionDto.PaymentMethodId, transactionDto.Amount);
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
            var p = await _paymenttransaction.FindAsync(x => x.PaymentId == dto.Id, false, x => x.Payment!);
            return _mapper.Map<IEnumerable<PaymentTransactionDto>>(p);

        }

         public async Task<IEnumerable<PaymentTransactionDto>> GetPaymentTransactionDtosByMethod(BaseDto<int> dto)
        {
            var t = await _paymenttransaction.FindAsync(x => x.PaymentMethodId == dto.Id, false, p => p.PaymentMethod!);

            return _mapper.Map<IEnumerable<PaymentTransactionDto>>(t);
        }
    }
}
