using Application.Common;
using Application.DTOs.CarBooking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IApplicationServices.CarBooking
{
    public interface ICarBookingService
    {
        /// <summary>
        /// Creates a new CarBooking.
        /// </summary>
        Task<CarBookingDto> CreateCarBookingAsync(CarBookingDto dto);

        /// <summary>
        /// Updates an existing CarBooking.
        /// </summary>
        Task<CarBookingDto> UpdateCarBookingAsync(UpdateCarBookingDto dto);

        /// <summary>
        /// Gets a CarBooking by its composite key: BookingId and CarId.
        /// </summary>
        Task<CarBookingDto> GetCarBookingByIdAsync(BaseDto<int> dto);

        /// <summary>
        /// Gets a list of CarBookings filtered by optional criteria.
        /// </summary>
        Task<IEnumerable<CarBookingDto>> GetCarBookingsByFilterAsync(CarBookingFilter? filter);

        /// <summary>
        /// Deletes a CarBooking by its composite key: BookingId and CarId.
        /// </summary>
        Task DeleteCarBookingAsync(BaseDto<int> dto);
    }
}
