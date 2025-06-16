<<<<<<< HEAD
﻿using System;
=======
﻿using Application.Common;
using Application.DTOs.CarBooking;
using System;
>>>>>>> AlaaWork
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
<<<<<<< HEAD
using Application.DTOs.Booking;
using Application.DTOs.CarBooking;
using Application.DTOs.Common;
using Application.Filter;
=======
>>>>>>> AlaaWork

namespace Application.IApplicationServices.CarBooking
{
    public interface ICarBookingService
    {
<<<<<<< HEAD


        public Task<IEnumerable<CarBookingDto>> GetByFilterAsync(CarBookingFilter? filter);
        public Task<CarBookingDto> GetByIdAsync(BaseDto<int> dto);


        public Task<CarBookingDto> CreateAsync(CreateCarBookingDto createCarBookingDto);

        public Task<CarBookingDto> UpdateAsync(UpdateCarBookingDto updateCarBookingDto);

        public Task DeleteByIdAsync(BaseDto<int> dto);
=======
        /// <summary>
        /// Creates a new CarBooking.
        /// </summary>
        Task<CarBookingDto> CreateCarBookingAsync(CarBookingDto dto);

        /// <summary>
        /// Updates an existing CarBooking.
        /// </summary>
        Task<CarBookingDto> UpdateCarBookingAsync(UpdateCarBookingDto dto);

        /// <summary>
        /// Gets a CarBooking by its composite key: BookingId and CarId.
        /// </summary>
        Task<CarBookingDto> GetCarBookingByIdAsync(BaseDto<int> dto);

        /// <summary>
        /// Gets a list of CarBookings filtered by optional criteria.
        /// </summary>
        Task<IEnumerable<CarBookingDto>> GetCarBookingsByFilterAsync(CarBookingFilter? filter);

        /// <summary>
        /// Deletes a CarBooking by its composite key: BookingId and CarId.
        /// </summary>
        Task DeleteCarBookingAsync(BaseDto<int> dto);
>>>>>>> AlaaWork
    }
}
