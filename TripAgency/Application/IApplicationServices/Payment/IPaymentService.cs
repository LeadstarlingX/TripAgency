using Application.Common;
using Application.DTOs.Payment;
using Application.DTOs.PaymentMethod;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IApplicationServices.Payment
{
    public  interface IPaymentService
    {
        Task<IEnumerable<PaymentDto>> GetPaymentsAsync();
        Task<PaymentDto> GetPaymentByIdAsync(BaseDto<int> dto);
        Task<PaymentDto> CreatePaymentAsync(CreatePaymentDto createPaymentDto);
        Task<PaymentDto> UpdatePayment(UpdatePaymentDto updatePaymentDto);
        Task<PaymentDto> DeletePaymentAsync(BaseDto<int> dto);










    }
}
