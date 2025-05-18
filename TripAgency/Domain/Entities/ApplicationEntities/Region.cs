using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.ApplicationEntities
{
    public class Region : BaseEntity
    {
        public Region()
        {
            TripPlans = new HashSet<TripPlan>();
        }
        public string Name { get; set; } = string.Empty;
        public virtual ICollection<TripPlan> TripPlans { get; set; }
    }
}
