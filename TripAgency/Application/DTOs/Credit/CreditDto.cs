using Application.Common;
using Application.DTOs.Customer;
using Application.DTOs.PaymentMethod;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Credit
{
    public class CreditDto : BaseDto<long>
    {
        public long CustomerId { get; set; }
        public int PaymentMethodId { get; set; }
        public decimal CreditAmount { get; set; }
        public bool IsActive { get; set; }
        
        // Navigation properties
        public CustomerDto? Customer { get; set; }
        public PaymentMethodDto? PaymentMethod { get; set; }
    }

    public class CreditsDto
    {
        public IList<CreditDto> Credits { get; set; } = [];
    }
} 