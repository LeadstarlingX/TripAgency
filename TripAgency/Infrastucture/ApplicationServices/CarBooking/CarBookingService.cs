using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Booking;
using Application.DTOs.CarBooking;
using Application.DTOs.Common;
using Application.Filter;
using Application.IApplicationServices.CarBooking;
using Application.IReositosy;
using AutoMapper;
using Domain.Entities.ApplicationEntities;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.ApplicationServices
{
    public class CarBookingService : ICarBookingService
    {
        private readonly IAppRepository<CarBooking> _carBookingRepository;
        private readonly IMapper  _mapper;

        public CarBookingService(IAppRepository<CarBooking> carBookingRepository,
            IMapper mapper)
        {
            _carBookingRepository = carBookingRepository;
            _mapper = mapper;
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

    }
}
