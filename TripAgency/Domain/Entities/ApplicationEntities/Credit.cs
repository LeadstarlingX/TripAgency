using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.ApplicationEntities
{
    public class Credit : BaseEntity
    {
        public long CustomerId { get; set; }
        public int PaymentMethodId { get; set; }
        public decimal CreditAmount { get; set; }
        public bool IsActive { get; set; } = true;

        // Navigation properties
        public virtual Customer? Customer { get; set; }
        public virtual PaymentMethod? PaymentMethod { get; set; }
    }
} 