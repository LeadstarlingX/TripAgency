using Application.Common;
using Application.DTOs.Payment;
using Application.IApplicationServices.Booking;
using Application.IApplicationServices.Car;
using Application.IApplicationServices.CarBooking;
using Application.IApplicationServices.Payment;
using Application.IReositosy;
using AutoMapper;
using Domain.Entities.ApplicationEntities;
using Domain.Enum;
using Infrastructure.Repository;
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

        public async Task<PaymentDto> UpdatePayment(UpdatePaymentDto updatePaymentDto)
        {
            var p = _mapper.Map<Payment>(updatePaymentDto);
            await _Repo.UpdateAsync(p);
            return _mapper.Map<PaymentDto>(p);
        }
    }
}
