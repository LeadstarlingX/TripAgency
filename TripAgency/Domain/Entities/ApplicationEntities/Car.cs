using Domain.Entities.Common;
using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.ApplicationEntities
{
    public partial class Car : BaseEntity
    {
        public Car()
        {
            CarBookings = new HashSet<CarBooking>();
            TripPlanCars = new HashSet<TripPlanCar>();
        }
        public string Model { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public string Color { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public CarStatusEnum CarStatus { get; set; }
        public decimal Pph { get; set; } // Price per hour
        public decimal Ppd { get; set; } // Price per day
        public decimal Mbw { get; set; }

        public Category? Category { get; set; }
        public virtual ICollection<CarBooking> CarBookings { get; set; }
        public virtual ICollection<TripPlanCar> TripPlanCars { get; set; }
    }
}