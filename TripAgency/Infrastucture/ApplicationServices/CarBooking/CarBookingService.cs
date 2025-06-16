using Application.Common;
using Application.DTOs.CarBooking;
using Application.IApplicationServices.CarBooking;
using Application.IReositosy;
using AutoMapper;
using Domain.Entities.ApplicationEntities;
using Domain.Enum;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ApplicationServices.CarBooking
{
    public class CarBookingService : ICarBookingService
    {
        private readonly IAppRepository<Domain.Entities.ApplicationEntities.CarBooking> _carBookingRepository;
        private readonly IAppRepository<Domain.Entities.ApplicationEntities.Car> _carRepository;

        private readonly IMapper _mapper;

        public CarBookingService(IAppRepository<Domain.Entities.ApplicationEntities.CarBooking> carBookingRepository,
            IAppRepository<Domain.Entities.ApplicationEntities.Car> carRepository,
            IMapper mapper)
        {
            _carBookingRepository = carBookingRepository;
            _mapper = mapper;

            _carRepository = carRepository;
        }
        public async Task<CarBookingDto> CreateCarBookingAsync(CarBookingDto dto)
        {
            if (dto == null || dto is null)
                throw new ArgumentNullException(nameof(dto));

            var carBooking = _mapper.Map<Domain.Entities.ApplicationEntities.CarBooking>(dto);
            var createdEntity = await _carBookingRepository.InsertAsync(carBooking);

            // Map and return
            return _mapper.Map<CarBookingDto>(createdEntity);
        }

        public async Task DeleteCarBookingAsync(BaseDto<int> dto)
        {
            var carBooking =( await _carBookingRepository.FindAsync(cb => cb.BookingId == dto.Id, false, cb => cb.Booking!,cb => cb.Car!)).FirstOrDefault();
            if (carBooking == null)
                throw new KeyNotFoundException($"CarBooking with ID {dto.Id} not found.");     
            
            await _carBookingRepository.RemoveAsync(carBooking);

            if ((carBooking.Booking!.Status == BookingStatusEnum.Cancelled || carBooking.Booking.Status == BookingStatusEnum.Completed))
            {
                carBooking.Car!.CarStatus = CarStatusEnum.Available;
                await _carRepository.UpdateAsync(carBooking.Car);
            }
        }

        public async Task<CarBookingDto> GetCarBookingByIdAsync(BaseDto<int> dto)
        {
            var carBooking = (await _carBookingRepository.FindAsync(cb => cb.BookingId == dto.Id, false, cb => cb.Booking!,cb => cb.Car!)).FirstOrDefault();
            if(carBooking == null)
                throw new KeyNotFoundException($"CarBooking with ID {dto.Id} not found.");

            return _mapper.Map<CarBookingDto>(carBooking);
        }

        public async Task<IEnumerable<CarBookingDto>> GetCarBookingsByFilterAsync(CarBookingFilter? filter)
        {
            var carBookings = await _carBookingRepository.GetAll(false,
                cb => cb.Booking!,
                cb => cb.Car!
            ).ToListAsync();

            return _mapper.Map<IEnumerable<CarBookingDto>>(carBookings);
        }

        public Task<CarBookingDto> UpdateCarBookingAsync(UpdateCarBookingDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
