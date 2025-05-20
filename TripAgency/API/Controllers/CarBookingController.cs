using Application.DTOs.Actions;
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

    }
}
