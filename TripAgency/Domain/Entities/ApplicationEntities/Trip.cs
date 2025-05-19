using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.ApplicationEntities
{
    public class Trip : BaseEntity
    {

        public Trip()
        {
            TripPlans = new HashSet<TripPlan>();
        }
        public string Name { get; set; } = string.Empty;
        public bool IsAvailable { get; set; }
        public string Description { get; set; } = string.Empty;
        public bool IsPrivate { get; set; }
        public string Slug { get; set; } = string.Empty;
        public virtual ICollection<TripPlan> TripPlans { get; set; }
    }
}