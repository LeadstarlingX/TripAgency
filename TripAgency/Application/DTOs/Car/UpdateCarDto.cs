using Application.Common;
using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Car
{
    public class UpdateCarDto:BaseDto<int>
    {

        public string Model { get; set; }
        public int Capacity { get; set; }
        public string Color { get; set; }
        public string Image { get; set; }
        public int CategoryId { get; set; }
        public CarStatusEnum CarStatus { get; set; }
        public decimal Pph { get; set; } // Price per hour
        public decimal Ppd { get; set; } // Price per day
        public decimal Mbw { get; set; }
    }
}
