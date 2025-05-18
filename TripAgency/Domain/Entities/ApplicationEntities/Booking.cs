using Domain.Entities.Common;
using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.ApplicationEntities
{
    public class Booking : BaseEntity
    {
        public Booking()
        {
            Payments = new HashSet<Payment>();
        }
        public long CustomerId { get; set; }
        public long Employeeid { get; set; }

        public string? BookingType { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public BookingStatusEnum Status { get; set; }
        public int NumOfPassengers { get; set; }

        public virtual CarBooking? CarBooking { get; set; }
        public virtual TripBooking? TripBooking { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }

        public Customer? Customer { get; set; }
        public Employee? Employee { get; set; }

    }
}
