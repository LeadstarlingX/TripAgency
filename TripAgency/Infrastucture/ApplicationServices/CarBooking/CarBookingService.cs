<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Booking;
using Application.DTOs.CarBooking;
using Application.DTOs.Common;
using Application.Filter;
=======
﻿using Application.Common;
using Application.DTOs.CarBooking;
>>>>>>> AlaaWork
using Application.IApplicationServices.CarBooking;
using Application.IReositosy;
using AutoMapper;
using Domain.Entities.ApplicationEntities;
<<<<<<< HEAD
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.ApplicationServices
{
    public class CarBookingService : ICarBookingService
    {
        private readonly IAppRepository<CarBooking> _carBookingRepository;
        private readonly IMapper  _mapper;

        public CarBookingService(IAppRepository<CarBooking> carBookingRepository,
=======
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
>>>>>>> AlaaWork
            IMapper mapper)
        {
            _carBookingRepository = carBookingRepository;
            _mapper = mapper;
<<<<<<< HEAD
        }

        #region Get
        public async Task<IEnumerable<CarBookingDto>> GetByFilterAsync(CarBookingFilter? filter)
        {
            var query = _carBookingRepository.GetAll();

            if(filter != null)
            {
                if (filter.CarId != null)
                    query = query.Where(x => x.CarId == filter.CarId);

                if (filter.BookingId != null)
                    query = query.Where(x => x.BookingId == filter.BookingId);

                if (filter.PickupLocation != null)
                    query = query.Where(x => x.PickupLocation == filter.PickupLocation);

                if (filter.DropoffLocation != null)
                    query = query.Where(x => x.DropoffLocation == filter.DropoffLocation);

                if (filter.WithDriver != null)
                    query = query.Where(x => x.WithDriver == filter.WithDriver);
            }

            var result = await query.ToListAsync();
            return _mapper.Map<IEnumerable<CarBookingDto>>(result);
        }

        public async Task<CarBookingDto> GetByIdAsync(BaseDto<int> dto)
        {
            var carBooking = (await _carBookingRepository.FindAsync(x => x.Id == dto.Id)).FirstOrDefault();
            if (carBooking == null)
            {
                throw new KeyNotFoundException("CarBooking Not Found");
            }
            return _mapper.Map<CarBookingDto>(carBooking);
        }

        #endregion

        public async Task<CarBookingDto> CreateAsync(CreateCarBookingDto createCarBookingDto)
        {
            var b = _mapper.Map<CarBooking>(createCarBookingDto);
            await _carBookingRepository.InsertAsync(b);
            return _mapper.Map<CarBookingDto>(b);
        }

        public async Task<CarBookingDto> UpdateAsync(UpdateCarBookingDto updateCarBookingDto)
        {
            var result = _mapper.Map<CarBooking>(updateCarBookingDto);
            await _carBookingRepository.UpdateAsync(result);
            return _mapper.Map<CarBookingDto>(result);
        }

        public async Task DeleteByIdAsync(BaseDto<int> dto)
        {
            var booking = (await _carBookingRepository.FindAsync(x => x.Id == dto.Id)).FirstOrDefault();
            if (booking == null)
                throw new KeyNotFoundException("Booking Not found");

            await _carBookingRepository.RemoveAsync(booking);
        }

=======
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
            var carBooking =( await _carBookingRepository.FindAsync(cb => cb.BookingId == dto.Id, cb => cb.Booking!,cb => cb.Car!)).FirstOrDefault();
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
            var carBooking = (await _carBookingRepository.FindAsync(cb => cb.BookingId == dto.Id, cb => cb.Booking!,cb => cb.Car!)).FirstOrDefault();
            if(carBooking == null)
                throw new KeyNotFoundException($"CarBooking with ID {dto.Id} not found.");

            return _mapper.Map<CarBookingDto>(carBooking);
        }

        public async Task<IEnumerable<CarBookingDto>> GetCarBookingsByFilterAsync(CarBookingFilter? filter)
        {
            var carBookings = await _carBookingRepository.GetAll(
                cb => cb.Booking!,
                cb => cb.Car!
            ).ToListAsync();

            return _mapper.Map<IEnumerable<CarBookingDto>>(carBookings);
        }

        public Task<CarBookingDto> UpdateCarBookingAsync(UpdateCarBookingDto dto)
        {
            throw new NotImplementedException();
        }
>>>>>>> AlaaWork
    }
}
