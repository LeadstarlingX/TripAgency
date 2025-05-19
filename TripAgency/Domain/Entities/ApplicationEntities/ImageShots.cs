using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.ApplicationEntities
{
    public class ImageShot : BaseEntity
    {
        public string? Path { get; set; }
        public string? Type { get; set; }
        public int CarBookingId { get; set; }
        public CarBooking? CarBooking { get; set; }
    }
}
