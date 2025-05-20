using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Booking;
using Application.DTOs.Common;
using Application.Filter;

namespace Application.IApplicationServices.Booking
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBookingService
    {
        /// <summary>
        /// Gets the bookings by filter asynchronous.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        Task<IEnumerable<BookingDto>> GetByFilterAsync(BookingFilter? filter);
        /// <summary>
        /// Gets the booking by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<BookingDto> GetByIdAsync(BaseDto<int> id);

        /// <summary>
        /// Creates the booking asynchronous.
        /// </summary>
        /// <param name="createCarDto">The create car dto.</param>
        /// <returns></returns>
        Task<BookingDto> CreateAsync(CreateBookingDto createBookingDto);
        /// <summary>
        /// Updates the booking asynchronous.
        /// </summary>
        /// <param name="updatecarDto">The updatecar dto.</param>
        /// <returns></returns>
        Task<BookingDto> UpdateAsync(UpdateBookingDto updateBookingDto);
        /// <summary>
        /// Deletes the booking by identifier asynchronous.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <returns></returns>
        Task DeleteByIdAsync(BaseDto<int> dto);
    }
}
