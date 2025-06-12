using Application.Common;
using Application.DTOs.Booking;
using Application.Filter;
using Application.IApplicationServices.Booking;
using Application.IReositosy;
using AutoMapper;
using Domain.Entities.ApplicationEntities;
using Domain.Enum;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

public class BookingService : IBookingService
{
    private readonly IAppRepository<Booking> _bookingRepository;
    private readonly IMapper _mapper;

    public BookingService(IAppRepository<Booking> bookingRepository, IMapper mapper)
    {
        _bookingRepository = bookingRepository;
        _mapper = mapper;
    }

    public async Task<BookingDto> CreateBookingAsync(CreateBookingDto createBookingDto)
    {
       
        var bookingEntity = _mapper.Map<Booking>(createBookingDto);
        bookingEntity.Status = BookingStatusEnum.Pending;

        var newBooking = await _bookingRepository.InsertAsync(bookingEntity);

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
        Func<IQueryable<Booking>, IQueryable<Booking>> includeExpression =
            query => query.Include(b => b.Customer).Include(b => b.Employee);

        var booking = await _bookingRepository
            .FindWithComplexIncludes(b => b.Id == id.Id, includeExpression)
            .FirstOrDefaultAsync();

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
                query = query.Where(b => b.BookingType == filter.BookingType);

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

        if (bookingToDelete == null)
        {
            throw new KeyNotFoundException($"Booking with ID {dto.Id} not found.");
        }

        if (bookingToDelete.Status == BookingStatusEnum.InProgress || bookingToDelete.Status == BookingStatusEnum.Completed)
        {
            throw new InvalidOperationException("Cannot delete a booking that is in progress or already completed. Please cancel it instead if necessary.");
        }

        await _bookingRepository.RemoveAsync(bookingToDelete);
    }
}