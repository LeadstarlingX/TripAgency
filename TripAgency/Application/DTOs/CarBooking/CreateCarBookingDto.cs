<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;
=======
﻿using Application.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
>>>>>>> AlaaWork
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.CarBooking
{
<<<<<<< HEAD
    public class CreateCarBookingDto
    {
        public int BookingId { get; set; }
        public int CarId { get; set; }
        public string PickupLocation { get; set; } = string.Empty;
        public string DropoffLocation { get; set; } = string.Empty;
=======
    public class CreateCarBookingDto : BaseDto<int>
    {
        [Required]
        public long CustomerId { get; set; }

        public long? EmployeeId { get; set; }

        [Required]
        public int CarId { get; set; }

        [Required]
        [StringLength(200)]
        public string PickupLocation { get; set; }

        [Required]
        [StringLength(200)]
        public string DropoffLocation { get; set; }

        [Required]
        public DateTime StartDateTime { get; set; }

        [Required]
        public DateTime EndDateTime { get; set; }

        [Required]
        [Range(1, 100)]
        public int NumOfPassengers { get; set; }

>>>>>>> AlaaWork
        public bool WithDriver { get; set; }
    }
}
