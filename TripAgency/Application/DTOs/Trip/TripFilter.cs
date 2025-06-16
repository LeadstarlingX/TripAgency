using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Trip
{
    public class TripFilter
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool? IsAvailable { get; set; }
        public bool? IsPrivate { get; set; }
        public string? Slug { get; set; }
    }
}
