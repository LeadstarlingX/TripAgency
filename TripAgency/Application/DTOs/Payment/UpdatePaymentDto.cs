using Application.DTOs.Common;
using DataAccessLayer.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Payment
{
    public class UpdatePaymentDto:BaseDto<int>
    {
        public int BookingId { get; set; }
        public PaymentStatusEnum Status { get; set; }
        public decimal AmountDue { get; set; }
        public decimal AmountPaid { get; set; }
        public DateTime PaymentDate { get; set; }
        public string Notes { get; set; } = string.Empty;
    }
}
