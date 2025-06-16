<<<<<<< HEAD
﻿using Application.DTOs.Actions;
using Application.DTOs.Booking;
using Application.DTOs.CarBooking;
using Application.DTOs.Common;
using Application.Filter;
using Application.IApplicationServices.CarBooking;
using Application.Serializer;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CarBookingController : ControllerBase
    {
        private readonly ICarBookingService _carBookingService;
        private readonly IJsonFieldsSerializer _jsonFieldsSerializer;

        public CarBookingController(ICarBookingService carBookingService, IJsonFieldsSerializer jsonFieldsSerializer)
        {
            _carBookingService = carBookingService;
            _jsonFieldsSerializer = jsonFieldsSerializer;
        }

        #region Get

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<List<BookingDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllCarBookings()
        {
            var result = await _carBookingService.GetByFilterAsync(null);

            if (result == null)
                return new RawJsonActionResult(
                    _jsonFieldsSerializer.Serialize(
                        new ApiResponse(true, "There are no carbookings", StatusCodes.Status200OK),
                        string.Empty));

            return new RawJsonActionResult(
                    _jsonFieldsSerializer.Serialize(
                        new ApiResponse(true, "", StatusCodes.Status200OK, result),
                        string.Empty));

        }


        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<List<BookingDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllBookingsByFilter([FromQuery] CarBookingFilter filter)
        {
            var result = await _carBookingService.GetByFilterAsync(filter);

            if (result == null)
                return new RawJsonActionResult(
                    _jsonFieldsSerializer.Serialize(
                        new ApiResponse(true, "There are no carbookings", StatusCodes.Status200OK),
                        string.Empty));

            return new RawJsonActionResult(
                    _jsonFieldsSerializer.Serialize(
                        new ApiResponse(true, "", StatusCodes.Status200OK, result),
                        string.Empty));
        }


        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<List<BookingDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCarBookingById([FromQuery] BaseDto<int> dto)
        {
                var result = await _carBookingService.GetByIdAsync(dto);

                return new RawJsonActionResult(
                        _jsonFieldsSerializer.Serialize(
                            new ApiResponse(true, "", StatusCodes.Status200OK, result),
                            string.Empty));

        }

        #endregion


        [HttpPut]
        [ProducesResponseType(typeof(ApiResponse<List<BookingDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCarBooking([FromQuery] UpdateCarBookingDto dto)
        {
            var result = await _carBookingService.UpdateAsync(dto);

            return new RawJsonActionResult(
                _jsonFieldsSerializer.Serialize(
                    new ApiResponse(true, "", StatusCodes.Status200OK, result),
                    string.Empty));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<List<BookingDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateCarBooking([FromQuery] CreateCarBookingDto dto)
        {
            await _carBookingService.CreateAsync(dto);

            return new RawJsonActionResult(
                    _jsonFieldsSerializer.Serialize(
                        new ApiResponse(true, "", StatusCodes.Status200OK),
                        string.Empty));
        }


        [HttpDelete]
        [ProducesResponseType(typeof(ApiResponse<List<BookingDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteByIdAsync([FromQuery] BaseDto<int> dto)
        {
            _carBookingService.DeleteByIdAsync(dto);
            return new RawJsonActionResult(
                _jsonFieldsSerializer.Serialize(
                    new ApiResponse(true, "", StatusCodes.Status200OK),
                    string.Empty));
        }

=======
﻿using Application.IApplicationServices.Authentication;
using Application.IApplicationServices.Booking;
using Application.IApplicationServices.Car;
using Application.IApplicationServices.CarBooking;
using Application.Serializer;
using Infrastructure.ApplicationServices.CarBooking;
using Infrastructure.ApplicationServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.Common;
using Application.DTOs.Actions;
using Application.DTOs.Booking;
using Application.DTOs.CarBooking;
using Domain.Entities.ApplicationEntities;
using Domain.Enum;
using Application.DTOs.Car;

namespace API.Controllers
{
    public class CarBookingController : BaseAuthenticatedController
    {
        private readonly IBookingService _bookingService;
        private readonly ICarBookingService _carBookingService;
        private readonly ICarService _carService;

        public CarBookingController(IBookingService bookingService,
        ICarBookingService carBookingService,
        ICarService carService,IAuthenticationService authenticationService,
        IJsonFieldsSerializer jsonFieldsSerializer) : base(authenticationService, jsonFieldsSerializer)
        {
            _bookingService = bookingService;
            _carBookingService = carBookingService;
            _carService = carService;
        }
        #region CreateCarBooking
        [HttpPost]
        public async Task<IActionResult> CreateCarBooking([FromBody] CreateCarBookingDto dto)
        {
            var car = await _carService.GetCarByIdAsync(new BaseDto<int> { Id = dto.CarId });
            if (car == null)
                throw new KeyNotFoundException($"Car with ID {dto.CarId} not found.");
            if (car.CarStatus == CarStatusEnum.NotAvailable)
                return new RawJsonActionResult(
                 _jsonFieldsSerializer.Serialize(
                     new ApiResponse(false, $"Car with ID {dto.CarId} not available.", StatusCodes.Status400BadRequest), string.Empty));
            var bookingDto = new CreateBookingDto
            {
                CustomerId = dto.CustomerId,
                EmployeeId = dto.EmployeeId,
                BookingType = "CarBooking",
                StartDateTime = dto.StartDateTime,
                EndDateTime = dto.EndDateTime,
                NumOfPassengers = dto.NumOfPassengers,
            };

            var booking = await _bookingService.CreateBookingAsync(bookingDto, await _authenticationService.GetAuthenticatedUser());
            var carBooking = new CarBookingDto
            {
                BookingId = booking.Id,
                CarId = dto.CarId,
                PickupLocation = dto.PickupLocation,
                DropoffLocation = dto.DropoffLocation,
                WithDriver = dto.WithDriver
            };

            var result = await _carBookingService.CreateCarBookingAsync(carBooking);
            if (result is not null)
            {
                var updateCar = new UpdateCarDto()
                {
                    Id = car.Id,
                    CarStatus = CarStatusEnum.NotAvailable
                };
                await _carService.UpdateCarAsync(updateCar);
            }
            return new RawJsonActionResult(
                _jsonFieldsSerializer.Serialize(
                    new ApiResponse(true, "Car booking created successfully", StatusCodes.Status200OK, result),
                    string.Empty));
        }
        #endregion

        [HttpGet]
        public async Task<IActionResult> GetAllCarBookings()
        {
            var result = await _carBookingService.GetCarBookingsByFilterAsync(null);

            return new RawJsonActionResult(
                _jsonFieldsSerializer.Serialize(
                    new ApiResponse(true, "Car bookings retrieved successfully", StatusCodes.Status200OK, result),
                    string.Empty));
        }


        [HttpPut]
        public async Task<IActionResult> UpdateCarBooking([FromBody] UpdateCarBookingDto dto)
        {
            var result = await _carBookingService.UpdateCarBookingAsync(dto);

            return new RawJsonActionResult(
                _jsonFieldsSerializer.Serialize(
                    new ApiResponse(true, "CarBooking updated successfully", StatusCodes.Status200OK, result),
                    string.Empty));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCarBooking([FromQuery]BaseDto<int> dto)
        {
            await _carBookingService.DeleteCarBookingAsync(dto);

            return new RawJsonActionResult(
                _jsonFieldsSerializer.Serialize(
                    new ApiResponse(true, "CarBooking deleted successfully", StatusCodes.Status200OK),
                    string.Empty));
        }

        [HttpGet]
        public async Task<IActionResult> GetCarBookingById([FromQuery] BaseDto<int> dto)
        {
            var result = await _carBookingService.GetCarBookingByIdAsync(dto);

            return new RawJsonActionResult(
                _jsonFieldsSerializer.Serialize(
                    new ApiResponse(true, "CarBooking retrieved successfully", StatusCodes.Status200OK, result),
                    string.Empty));
        }
>>>>>>> AlaaWork
    }
}
