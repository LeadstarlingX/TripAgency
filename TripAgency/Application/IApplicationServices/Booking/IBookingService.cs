using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common;
using Application.DTOs.Authentication;
using Application.DTOs.Booking;
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
        Task<IEnumerable<BookingDto>> GetBookingsByFilterAsync(BookingFilter? filter);
        /// <summary>
        /// Gets the booking by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<BookingDto> GetBookingByIdAsync(BaseDto<int> id);

        /// <summary>
        /// Creates the booking asynchronous.
        /// </summary>
        /// <param name="createCarDto">The create car dto.</param>
        /// <returns></returns>
        Task<BookingDto> CreateBookingAsync(CreateBookingDto createBookingDto, UserProfileDto currentUser);
        /// <summary>
        /// Updates the booking asynchronous.
        /// </summary>
        /// <param name="updatecarDto">The updatecar dto.</param>
        /// <returns></returns>
        Task<BookingDto> UpdateBookingAsync(UpdateBookingDto updateBookingDto);
        /// <summary>
        /// Deletes the booking by identifier asynchronous.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <returns></returns>
        Task DeleteBookingByIdAsync(BaseDto<int> dto);
        Task<BookingDto> ConfirmBookingAsync(int bookingId, long employeeId);

    }
}