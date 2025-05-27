using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using Application.Common;
using Application.DTOs.Booking;
using Application.DTOs.Car;
using Application.Filter;
using Application.IApplicationServices.Booking;
using Application.IReositosy;
using AutoMapper;
using Domain.Entities.ApplicationEntities;
using Domain.Enum;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.ApplicationServices
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Application.IApplicationServices.Booking.IBookingService" />
    public class BookingService : IBookingService
    {
        /// <summary>
        /// The booking repositry
        /// </summary>
        private readonly IAppRepository<Booking> _bookingRepositry;
        /// <summary>
        /// The mapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="BookingService"/> class.
        /// </summary>
        /// <param name="bookingRepositry">The booking repositry.</param>
        /// <param name="mapper">The mapper.</param>
        public BookingService(IAppRepository<Booking> bookingRepositry, IMapper mapper)
        {
            _bookingRepositry = bookingRepositry;
            _mapper = mapper;
        }

        #region Get
        /// <summary>
        /// Gets the bookings by filter asynchronous.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public async Task<IEnumerable<BookingDto>> GetBookingsByFilterAsync(BookingFilter? filter)
        {
            var query = _bookingRepositry.GetAllWithAllInclude();

            if(filter.CustomerId != 0)
                query = query.Where(x => x.CustomerId == filter.CustomerId);

            if (filter.EmployeeId != 0)
                query = query.Where(x => x.Employeeid == filter.EmployeeId);

            if (filter.NumOfPassengers != 0)
                query = query.Where(x => x.NumOfPassengers == filter.NumOfPassengers);

            if (filter.BookingType != null)
                query = query.Where(x => x.BookingType == filter.BookingType);

            if (filter.Payments != null)
                query = query.Where(x => x.Payments == filter.Payments);

            if (filter.Status != BookingStatusEnum.All)
                query = query.Where(x => x.Status == filter.Status);

            if (filter.StartDateTime != DateTime.MinValue)
                query = query.Where(x => x.StartDateTime >=  filter.StartDateTime);

            if (filter.EndDateTime != DateTime.MaxValue)
                query = query.Where(x => x.StartDateTime <= filter.StartDateTime);


            var result = await query.ToListAsync();
            return _mapper.Map<IEnumerable<BookingDto>>(result);
        }
        /// <summary>
        /// Gets the booking by identifier asynchronous.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <returns></returns>
        /// <exception cref="System.Collections.Generic.KeyNotFoundException">Booking Not Found</exception>
        public async Task<BookingDto> GetBookingByIdAsync(BaseDto<int> dto)
        {
            var booking = (await _bookingRepositry.FindAsync(x => x.Id == dto.Id)).FirstOrDefault();
            if (booking == null)
            {
                throw new KeyNotFoundException("Booking Not Found");
            }
            return _mapper.Map<BookingDto>(booking);
        }
        #endregion

        /// <summary>
        /// Creates the booking asynchronous.
        /// </summary>
        /// <param name="createBookingDto">The create booking dto.</param>
        /// <returns>
        /// A dto of the created booking
        /// </returns>
        public async Task<BookingDto> CreateBookingAsync(CreateBookingDto createBookingDto)
        {
            var b = _mapper.Map<Booking>(createBookingDto);
            await _bookingRepositry.InsertAsync(b);
            return _mapper.Map<BookingDto>(b);
        }

        /// <summary>
        /// Updates the booking asynchronous.
        /// </summary>
        /// <param name="updateBookingDto">The update booking dto.</param>
        /// <returns>
        /// A dto of the updated booking
        /// </returns>
        /// <exception cref="System.Collections.Generic.KeyNotFoundException">Booking Not found</exception>
        public async Task<BookingDto> UpdateBookingAsync(UpdateBookingDto updateBookingDto)
        {
            var result = (await _bookingRepositry.FindAsync(x => x.Id == updateBookingDto.Id)).FirstOrDefault();
            if(result == null)
                throw new KeyNotFoundException("Booking Not found");

            var b = await _bookingRepositry.UpdateAsync(_mapper.Map<Booking>(updateBookingDto));
            return _mapper.Map<BookingDto>(b);
        }
        /// <summary>
        /// Deletes the booking by identifier asynchronous.
        /// </summary>
        /// <param name="dto">The dto which handels the Id.</param>
        /// <exception cref="System.Collections.Generic.KeyNotFoundException">Booking Not found</exception>
        public async Task DeleteBookingByIdAsync(BaseDto<int> dto)
        {
            var booking = (await _bookingRepositry.FindAsync(x => x.Id == dto.Id)).FirstOrDefault();
            if(booking == null)
                throw new KeyNotFoundException("Booking Not found");

            await _bookingRepositry.RemoveAsync(booking);
        }
    }
}
