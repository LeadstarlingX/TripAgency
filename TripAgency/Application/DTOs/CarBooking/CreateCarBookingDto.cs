
﻿using Application.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.CarBooking
{

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

        public bool WithDriver { get; set; }
    }
}
