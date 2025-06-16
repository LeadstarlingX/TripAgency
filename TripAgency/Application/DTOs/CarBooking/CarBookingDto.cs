using Application.DTOs.Booking;
using Application.DTOs.Car;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Application.DTOs.CarBooking
{
    public class CarBookingDto
    {
        public int BookingId { get; set; }
        public int CarId { get; set; }
        public string PickupLocation { get; set; }
        public string DropoffLocation { get; set; }
        public bool WithDriver { get; set; }
        public BookingDto? Booking { get; set; }
        public CarDto? Car { get; set; }
    }
}
