<<<<<<< HEAD
﻿using System;
=======
﻿using Application.Common;
using System;
>>>>>>> AlaaWork
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
<<<<<<< HEAD
using Application.DTOs.Common;
=======
>>>>>>> AlaaWork

namespace Application.DTOs.CarBooking
{
    public class UpdateCarBookingDto : BaseDto<int>
    {
<<<<<<< HEAD
        public int BookingId { get; set; }
        public int CarId { get; set; }
        public string PickupLocation { get; set; } = string.Empty;
        public string DropoffLocation { get; set; } = string.Empty;
        public bool WithDriver { get; set; }
=======
        public int? CarId { get; set; }

        public string? PickupLocation { get; set; }

        public string? DropoffLocation { get; set; }

        public bool? WithDriver { get; set; }
>>>>>>> AlaaWork
    }
}
