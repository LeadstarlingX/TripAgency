using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common;
using Domain.Entities.ApplicationEntities;
using Domain.Enum;

namespace Application.DTOs.Booking
{
    public class UpdateBookingDto : BaseDto<int>
    {

        public string? BookingType { get; set; }
        public DateTime? StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
        [EnumDataType(typeof(BookingStatusEnum), ErrorMessage = "Invalid status value.")]
        public BookingStatusEnum? Status { get; set; }
        [Range(1, 100, ErrorMessage = "Number of passengers must be between 1 and 100.")]
        public int? NumOfPassengers { get; set; }
    }
}
