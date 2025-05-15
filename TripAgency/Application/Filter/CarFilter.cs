using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
