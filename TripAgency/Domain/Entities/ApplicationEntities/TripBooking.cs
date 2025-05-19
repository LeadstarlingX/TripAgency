using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.ApplicationEntities
{
    public class TripBooking
    {
        public int BookingId { get; set; }
        public int TripPlanId { get; set; }
        public bool WithGuide { get; set; }

        public virtual Booking? Booking { get; set; }
        public virtual TripPlan? TripPlan { get; set; }
    }
}
