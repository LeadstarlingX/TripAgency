using Application.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Trip
{
    public class UpdateTripDto : BaseDto<int>
    {
        [StringLength(100, MinimumLength = 3)]
        public string? Name { get; set; }

        public bool? IsAvailable { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        public bool? IsPrivate { get; set; }

        [StringLength(100)]
        public string? Slug { get; set; }
    }
}
