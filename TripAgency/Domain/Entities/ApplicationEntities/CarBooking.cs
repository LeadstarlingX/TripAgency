using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.ApplicationEntities
{
    public class CarBooking
    {
        public CarBooking()
        {
            ImageShots = new HashSet<ImageShot>();

        }
        public int BookingId { get; set; }
        public int CarId { get; set; }
        public string PickupLocation { get; set; } = string.Empty;
        public string DropoffLocation { get; set; } = string.Empty;
        public bool WithDriver { get; set; }
        public Booking? Booking { get; set; }
        public Car? Car { get; set; }
        public virtual ICollection<ImageShot> ImageShots { get; set; }
    }
}
