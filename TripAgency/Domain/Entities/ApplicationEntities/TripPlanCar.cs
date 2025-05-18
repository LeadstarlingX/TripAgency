using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.ApplicationEntities
{
    public class TripPlanCar : BaseEntity
    {
        public decimal Price { get; set; }
        public int TripPlanId { get; set; }
        public int CarId { get; set; }
        public TripPlan? TripPlan { get; set; }
        public Car? Car { get; set; }
    }
}
