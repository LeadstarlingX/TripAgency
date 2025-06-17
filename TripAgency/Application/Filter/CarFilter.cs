using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enum;

namespace Application.Filter
{
   public class CarFilter
    {
        public decimal? Pph { get; set; } // Price per hour
        public decimal? Ppd { get; set; } // Price per day
        public decimal? Mbw { get; set; }

        public string? Model { get; set; }
        public int? Capacity { get; set; }
        public string? Color { get; set; }
        public CarStatusEnum? Status { get; set; }
    }
}
