using Application.Common;
using Application.DTOs.Payment;
using Application.Filter;
using Application.IApplicationServices.Booking;
using Application.IApplicationServices.Car;
using Application.IApplicationServices.CarBooking;
using Application.IApplicationServices.Payment;
using Application.IReositosy;
using AutoMapper;
using Domain.Entities.ApplicationEntities;
using Domain.Enum;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ApplicationServices
{
    public class PaymentService : IPaymentService
    {
         private readonly IAppRepository<Payment> _Repo;
        private readonly IMapper _mapper;
        private readonly IBookingService _bookingService;
        private readonly ICarService _carService;
        private readonly ICarBookingService _carBookingService;

        public PaymentService(IAppRepository<Payment> payment ,IMapper mapper ,IBookingService bookingService , 
            ICarBookingService carBookingService ,ICarService carService)
        {  _mapper = mapper; 
            _bookingService = bookingService;
            _Repo=payment;
            _carBookingService=carBookingService;
            _carService=carService;

        }

        public async Task<PaymentDto> CreatePaymentAsync(CreatePaymentDto createPaymentDto)
        {
            var p = _mapper.Map<Payment>(createPaymentDto);

            var booking = await _bookingService.GetBookingByIdAsync(new BaseDto<int> { Id = createPaymentDto.BookingId});  
            DateTime start = booking.StartDateTime;
            DateTime end = booking.EndDateTime;         
            if(booking.BookingType== BookingTypes.TripBooking)
            {
                p.AmountDue = 1000m;
                p.AmountPaid = 0m;
            }
            else if(booking.BookingType== BookingTypes.CarBooking)
            {
                var carBooking = await _carBookingService.GetCarBookingByIdAsync(new BaseDto<int> { Id = createPaymentDto.BookingId });
                p.AmountPaid = 0m;
                p.AmountDue = 0; 
                var total = (end - start);
                if ( total.Days > 0)
                    p.AmountDue += (decimal)((total.Days) * carBooking.Car!.Ppd);
                
                if(total.Hours > 0)
                    p.AmountDue += (decimal)total.Hours * carBooking.Car!.Pph;
            }
            await _Repo.InsertAsync(p);
            return _mapper.Map<PaymentDto>(p);
        }

        public async Task<PaymentDto> DeletePaymentAsync(BaseDto<int> dto)
        {
            var p = (await _Repo.FindAsync(x=> x.Id == dto.Id)).FirstOrDefault();

            await _Repo.RemoveAsync(p);

            return _mapper.Map<PaymentDto>(p);
        }

        public async Task<PaymentDto> GetPaymentByIdAsync(BaseDto<int> dto, bool asNoTraking = false)
        {
            var p = (await _Repo.FindAsync(x => x.Id == dto.Id, asNoTraking)).FirstOrDefault();

            return _mapper.Map<PaymentDto>(p);
        }

        public async Task<IEnumerable<PaymentDto>> GetPaymentsAsync()
        {
            var p = await _Repo.GetAllWithAllIncludeAsync();
            return _mapper.Map<IEnumerable<PaymentDto>>(p);
        }

        public async Task<IEnumerable<PaymentDto>> GetPaymentsByFilterAsync(PaymentFilter? filter)
        {
            var query = _Repo.GetAll(false, p => p.Booking!, p => p.Booking!.Customer!, p => p.Booking!.Employee!);

            if (filter != null)
            {
                if (filter.Id.HasValue)
                    query = query.Where(p => p.Id == filter.Id.Value);

                if (filter.BookingId.HasValue)
                    query = query.Where(p => p.BookingId == filter.BookingId.Value);

                if (filter.Status.HasValue)
                    query = query.Where(p => p.Status == filter.Status.Value);

                if (filter.MinAmountDue.HasValue)
                    query = query.Where(p => p.AmountDue >= filter.MinAmountDue.Value);

                if (filter.MaxAmountDue.HasValue)
                    query = query.Where(p => p.AmountDue <= filter.MaxAmountDue.Value);

                if (filter.MinAmountPaid.HasValue)
                    query = query.Where(p => p.AmountPaid >= filter.MinAmountPaid.Value);

                if (filter.MaxAmountPaid.HasValue)
                    query = query.Where(p => p.AmountPaid <= filter.MaxAmountPaid.Value);

                if (filter.MinPaymentDate.HasValue)
                    query = query.Where(p => p.PaymentDate >= filter.MinPaymentDate.Value);

                if (filter.MaxPaymentDate.HasValue)
                    query = query.Where(p => p.PaymentDate <= filter.MaxPaymentDate.Value);

                if (!string.IsNullOrEmpty(filter.Notes))
                    query = query.Where(p => p.Notes.Contains(filter.Notes));

                if (filter.CustomerId.HasValue)
                    query = query.Where(p => p.Booking!.CustomerId == filter.CustomerId.Value);

                if (filter.EmployeeId.HasValue)
                    query = query.Where(p => p.Booking!.Employeeid == filter.EmployeeId.Value);
            }

            var payments = await query.OrderByDescending(p => p.PaymentDate).ToListAsync();
            return _mapper.Map<IEnumerable<PaymentDto>>(payments);
        }

        public async Task<PaymentDto> UpdatePayment(UpdatePaymentDto updatePaymentDto)
        {
            var p = _mapper.Map<Payment>(updatePaymentDto);
            await _Repo.UpdateAsync(p);
            return _mapper.Map<PaymentDto>(p);
        }
    }
}
