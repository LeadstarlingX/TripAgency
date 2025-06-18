using Application.Common;
using Application.DTOs.Credit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IApplicationServices.Credit
{
    public interface ICreditService
    {
        Task<CreditsDto> GetCreditsAsync();
        Task<CreditDto> GetCreditByIdAsync(BaseDto<long> dto);
        Task<CreditDto> CreateCreditAsync(CreateCreditDto createCreditDto);
        Task<CreditDto> UpdateCreditAsync(UpdateCreditDto updateCreditDto);
        Task DeleteCreditAsync(BaseDto<long> dto);
        Task<CreditsDto> GetCreditsByCustomerAsync(BaseDto<long> customerDto);
        Task<CreditsDto> GetCreditsByPaymentMethodAsync(BaseDto<int> paymentMethodDto);
    }
} 