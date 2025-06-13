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
        _bookingRepository = bookingRepository;
        _mapper = mapper;
        _employeeRepository = repository;
    }

    public async Task<BookingDto> CreateBookingAsync(CreateBookingDto createBookingDto, UserProfileDto currentUser)
    {
        var bookingEntity = _mapper.Map<Booking>(createBookingDto);
        bookingEntity.Status = BookingStatusEnum.Pending;

        var newBooking = await _bookingRepository.InsertAsync(bookingEntity);

        // Check if user is not a Customer
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