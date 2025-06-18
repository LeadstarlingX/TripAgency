using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.ApplicationEntities;
using Domain.Enum;

namespace Application.Filter
{
    public class PaymentFilter
    {
        public int? Id { get; set; }
        public int? BookingId { get; set; }
        public PaymentStatusEnum? Status { get; set; }
        public decimal? MinAmountDue { get; set; }
        public decimal? MaxAmountDue { get; set; }
        public decimal? MinAmountPaid { get; set; }
        public decimal? MaxAmountPaid { get; set; }
        public DateTime? MinPaymentDate { get; set; }
        public DateTime? MaxPaymentDate { get; set; }
        public string? Notes { get; set; }
        public long? CustomerId { get; set; }
        public long? EmployeeId { get; set; }
    }
} 