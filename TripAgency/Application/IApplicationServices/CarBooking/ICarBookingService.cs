using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Booking;
using Application.DTOs.CarBooking;
using Application.DTOs.Common;
using Application.Filter;

namespace Application.IApplicationServices.CarBooking
{
    public interface ICarBookingService
    {


        public Task<IEnumerable<CarBookingDto>> GetByFilterAsync(CarBookingFilter? filter);
        public Task<CarBookingDto> GetByIdAsync(BaseDto<int> dto);


        public Task<CarBookingDto> CreateAsync(CreateCarBookingDto createCarBookingDto);

        public Task<CarBookingDto> UpdateAsync(UpdateCarBookingDto updateCarBookingDto);

        public Task DeleteByIdAsync(BaseDto<int> dto);
    }
}
