using Application.DTOs.Common;
using Application.DTOs.PaymentMethod;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IApplicationServices
{
    public interface IPaymentMethodService
    {
        Task<IEnumerable<PaymentMethodDto>> GetPaymentMethodsAsync( );
        Task<PaymentMethodDto> GetPaymentMethodByIdAsync(BaseDto<int> dto);
        Task<PaymentMethodDto> CreatePaymentMethodAsync(CreatePaymentMethodDto createPaymentMethodDto);
        Task<PaymentMethodDto> UpdatePaymentMethod (UpdatePaymentMethodDto updatePaymentMethodDto);
        Task<PaymentMethodDto> DeletePaymentMethodAsync(BaseDto<int> dto);
    }
}
