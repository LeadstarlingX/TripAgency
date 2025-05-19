using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.ApplicationEntities;
using Domain.Enum;

namespace Application.Filter
{
    public class BookingFilter
    {
        public long CustomerId { get; set; }
        public long EmployeeId { get; set; }
        public string BookingType { get; set; }
        public DateTime StartDateTime { get; set; } = DateTime.MinValue;
        public DateTime EndDateTime { get; set; } = DateTime.MaxValue;
        public BookingStatusEnum Status { get; set; } = BookingStatusEnum.All;
        public int NumOfPassengers { get; set; }
        public ICollection<Payment> Payments { get; set; }
    }
}
