using Application.Common;
using Application.DTOs.Authentication;
using Application.DTOs.Booking;
using Application.Filter;
using Application.IApplicationServices.Booking;
using Application.IReositosy;
using AutoMapper;
using Domain.Common;
using Domain.Entities.ApplicationEntities;
using Domain.Enum;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

public class BookingService : IBookingService
{
    private readonly IAppRepository<Booking> _bookingRepository;
    private readonly IAppRepository<Employee> _employeeRepository;
    private readonly IMapper _mapper;

    public BookingService(IAppRepository<Booking> bookingRepository,IAppRepository<Employee> repository, IMapper mapper)
    {
<<<<<<< HEAD
        /// <summary>
        /// The booking repositry
        /// </summary>
        private readonly IAppRepository<Booking> _bookingRepository;
        /// <summary>
        /// The mapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="BookingService"/> class.
        /// </summary>
        /// <param name="bookingRepositry">The booking repositry.</param>
        /// <param name="mapper">The mapper.</param>
        public BookingService(IAppRepository<Booking> bookingRepository, IMapper mapper)
        {
            _bookingRepository = bookingRepository;
            _mapper = mapper;
        }

        #region Get
        /// <summary>
        /// Gets the bookings by filter asynchronous.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public async Task<IEnumerable<BookingDto>> GetByFilterAsync(BookingFilter? filter)
        {
                var query = _bookingRepository.GetAll();
            if (filter != null)
            {
                if (filter.CustomerId != 0)
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
                    query = query.Where(x => x.StartDateTime >= filter.StartDateTime);

                if (filter.EndDateTime != DateTime.MaxValue)
                    query = query.Where(x => x.EndDateTime <= filter.EndDateTime);
            }

            var result = await query.ToListAsync();
                return _mapper.Map<IEnumerable<BookingDto>>(result);

        }
        /// <summary>
        /// Gets the booking by identifier asynchronous.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <returns></returns>
        /// <exception cref="System.Collections.Generic.KeyNotFoundException">Booking Not Found</exception>
        public async Task<BookingDto> GetByIdAsync(BaseDto<int> dto)
        {
            var booking = (await _bookingRepository.FindAsync(x => x.Id == dto.Id)).FirstOrDefault();
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
        public async Task<BookingDto> CreateAsync(CreateBookingDto createBookingDto)
        {
            var b = _mapper.Map<Booking>(createBookingDto);
            await _bookingRepository.InsertAsync(b);
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
        public async Task<BookingDto> UpdateAsync(UpdateBookingDto updateBookingDto)
        {
            var result = _mapper.Map<Booking>(updateBookingDto);
            await _bookingRepository.UpdateAsync(result);
            return _mapper.Map<BookingDto>(result);
        }
        /// <summary>
        /// Deletes the booking by identifier asynchronous.
        /// </summary>
        /// <param name="dto">The dto which handels the Id.</param>
        /// <exception cref="System.Collections.Generic.KeyNotFoundException">Booking Not found</exception>
        public async Task DeleteByIdAsync(BaseDto<int> dto)
        {
            var booking = (await _bookingRepository.FindAsync(x => x.Id == dto.Id)).FirstOrDefault();
            if(booking == null)
                throw new KeyNotFoundException("Booking Not found");

            await _bookingRepository.RemoveAsync(booking);
        }
=======
        _bookingRepository = bookingRepository;
        _mapper = mapper;
        _employeeRepository = repository;
>>>>>>> AlaaWork
    }

    public async Task<BookingDto> CreateBookingAsync(CreateBookingDto createBookingDto, UserProfileDto currentUser)
    {
        var bookingEntity = _mapper.Map<Booking>(createBookingDto);
        bookingEntity.Status = BookingStatusEnum.Pending;

        var newBooking = await _bookingRepository.InsertAsync(bookingEntity);

        if (currentUser.Token != null &&
            !currentUser.Token.UserRoles.Any(r => r.Equals(DefaultSetting.CustomerRoleName, StringComparison.OrdinalIgnoreCase)))
        {
            if (currentUser.Token.UserRoles.Contains(DefaultSetting.AdminRoleName) ||  currentUser.Token.UserRoles.Contains(DefaultSetting.EmployeeRoleName))
            {
                var employee = (await _employeeRepository.FindAsync(e => e.UserId == currentUser.Id)).FirstOrDefault();

                if (employee != null)
                {
                    newBooking.Employeeid = employee.UserId;
                    newBooking.Status = BookingStatusEnum.InProgress;

                    await _bookingRepository.UpdateAsync(newBooking);
                }
            }
        }

        return await GetBookingByIdAsync(new BaseDto<int> { Id = (int)newBooking.Id });
    }

    public async Task<BookingDto> UpdateBookingAsync(UpdateBookingDto updateBookingDto)
    {
        var existingBooking = (await _bookingRepository.FindAsync(b => b.Id == updateBookingDto.Id)).FirstOrDefault();

        if (existingBooking == null)
        {
            throw new KeyNotFoundException($"Booking with ID {updateBookingDto.Id} not found to update.");
        }

        if (existingBooking.Status == BookingStatusEnum.Completed || existingBooking.Status == BookingStatusEnum.Cancelled)
        {
            throw new InvalidOperationException($"Cannot update a booking that is already '{existingBooking.Status}'.");
        }

        _mapper.Map(updateBookingDto, existingBooking);
        await _bookingRepository.UpdateAsync(existingBooking);

        return await GetBookingByIdAsync(new BaseDto<int> { Id = (int)existingBooking.Id });
    }

    public async Task<BookingDto> GetBookingByIdAsync(BaseDto<int> id)
    {
        var booking =(await _bookingRepository.FindAsync(b => b.Id == id.Id,b => b.Customer!, b => b.Employee!)).FirstOrDefault();


        if (booking == null)
        {
            throw new KeyNotFoundException($"Booking with ID {id.Id} not found.");
        }

        return _mapper.Map<BookingDto>(booking);
    }

    public async Task<IEnumerable<BookingDto>> GetBookingsByFilterAsync(BookingFilter? filter)
    {
        var query = _bookingRepository.GetAll(b => b.Customer!, b => b.Employee!);

        if (filter != null)
        {
            if (filter.CustomerId.HasValue)
                query = query.Where(b => b.CustomerId == filter.CustomerId.Value);

            if (filter.EmployeeId.HasValue)
                query = query.Where(b => b.Employeeid == filter.EmployeeId.Value);

            if (!string.IsNullOrEmpty(filter.BookingType))
                query = query.Where(b => b.BookingType!.Trim().ToLower() == filter.BookingType.Trim().ToLower());
            if (filter.Status.HasValue)
                query = query.Where(b => b.Status == filter.Status.Value);

            if (filter.NumOfPassengers.HasValue)
                query = query.Where(b => b.NumOfPassengers == filter.NumOfPassengers.Value);

            if (filter.StartDateTime.HasValue)
                query = query.Where(b => b.StartDateTime >= filter.StartDateTime.Value);

            if (filter.EndDateTime.HasValue)
                query = query.Where(b => b.EndDateTime <= filter.EndDateTime.Value);
        }

        var bookings = await query.OrderByDescending(b => b.StartDateTime).ToListAsync();

        return _mapper.Map<IEnumerable<BookingDto>>(bookings);
    }

    public async Task DeleteBookingByIdAsync(BaseDto<int> dto)
    {
        var bookingToDelete = (await _bookingRepository.FindAsync(b => b.Id == dto.Id)).FirstOrDefault();

        if (bookingToDelete is null)
        {
            throw new KeyNotFoundException($"Booking with ID {dto.Id} not found.");
        }

        if (bookingToDelete.Status == BookingStatusEnum.InProgress || bookingToDelete.Status == BookingStatusEnum.Completed)
        {
            throw new InvalidOperationException("Cannot delete a booking that is in progress or already completed. Please cancel it instead if necessary.");
        }

        await _bookingRepository.RemoveAsync(bookingToDelete);
    }

    public async Task<BookingDto> ConfirmBookingAsync(int bookingId, long employeeId)
    {
        var booking = (await _bookingRepository.FindAsync(b => b.Id == bookingId)).FirstOrDefault();

        if (booking == null)
        {
            throw new KeyNotFoundException($"Booking with ID {bookingId} not found.");
        }

        if (booking.Status != BookingStatusEnum.Pending)
        {
            throw new InvalidOperationException("Only pending bookings can be confirmed.");
        }

        booking.Employeeid = employeeId;
        booking.Status = BookingStatusEnum.InProgress;

        await _bookingRepository.UpdateAsync(booking);

        return _mapper.Map<BookingDto>(booking);
    }
}