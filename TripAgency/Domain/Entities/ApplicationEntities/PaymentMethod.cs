using DataAccessLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.ApplicationEntities
{
    public partial class PaymentMethod : BaseEntity
    {
        public string Method { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
        public ICollection<PaymentTransaction> PaymentTransctions { get; set; } = [];
    }
}
