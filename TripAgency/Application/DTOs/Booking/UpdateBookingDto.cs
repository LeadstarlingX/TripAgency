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
        [Required(ErrorMessage = "Customer ID is required.")]
        [Range(1, long.MaxValue, ErrorMessage = "Please provide a valid Customer ID.")]
        public long CustomerId { get; set; }

        [Required(ErrorMessage = "Employee ID is required.")]
        [Range(1, long.MaxValue, ErrorMessage = "Please provide a valid Employee ID.")]
        public long EmployeeId { get; set; }

        [StringLength(100, ErrorMessage = "Booking Type cannot exceed 100 characters.")]
        public string BookingType { get; set; }

        [Required(ErrorMessage = "Start date and time are required.")]
        public DateTime StartDateTime { get; set; }

        [Required(ErrorMessage = "End date and time are required.")]
        public DateTime EndDateTime { get; set; }

        [Required(ErrorMessage = "A booking status is required.")]
        [EnumDataType(typeof(BookingStatusEnum), ErrorMessage = "Invalid status value.")]
        public BookingStatusEnum Status { get; set; }

        [Required(ErrorMessage = "Number of passengers is required.")]
        [Range(1, 100, ErrorMessage = "Number of passengers must be between 1 and 100.")]
        public int NumOfPassengers { get; set; }
    }
}
