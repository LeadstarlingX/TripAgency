using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.ApplicationEntities
{
    public class TripPlan : BaseEntity
    {
        public TripPlan()
        {
            TripBookings = new HashSet<TripBooking>();
            TripPlanCars = new HashSet<TripPlanCar>();

        }
        public int RegionId { get; set; }
        public int TripId { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public decimal Duration { get; set; }
        public string IndudedServices { get; set; } = string.Empty;
        public string Stops { get; set; } = string.Empty;
        public string MealsPlan { get; set; } = string.Empty;
        public string HotelsStays { get; set; } = string.Empty;
        public virtual Region? Region { get; set; }
        public virtual Trip? Trip { get; set; }
        public virtual ICollection<TripBooking> TripBookings { get; set; }
        public virtual ICollection<TripPlanCar> TripPlanCars { get; set; }

    }
}
