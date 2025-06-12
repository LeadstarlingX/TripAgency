using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enum
{
    public enum BookingStatusEnum
    {
        // The booking request has been received but is awaiting confirmation.
        // This could be pending payment or admin approval.
        Pending = 1,

        // The booking has been confirmed, payment is successful (if required),
        // and resources (car, employee) are allocated.
        Confirmed = 2,

        // The booking is currently active.
        // For a car rental, the car is with the customer.
        // For a trip, the trip is underway.
        InProgress = 3,

        // The booking has been successfully completed.
        // The car has been returned, or the trip has ended.
        Completed = 4,

        // The booking was cancelled. This is a final state.
        // You might want to store the reason for cancellation elsewhere.
        Cancelled = 5,

        // The customer did not show up for the booking at the scheduled time.
        // This is distinct from a cancellation as it may have different fee implications.
        NoShow = 6
    }
}
