using Application.Common;
using Application.DTOs.Actions;
using Application.DTOs.Booking;
using Application.Filter;
using Application.IApplicationServices.Authentication;
using Application.IApplicationServices.Booking;
using Application.Serializer;
using Domain.Common;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// Controller for managing bookings, inherits from <see cref="BaseAuthenticatedController"/>.
    /// </summary>
    public class BookingController : BaseAuthenticatedController
    {
        private readonly IBookingService _bookingService;

        public BookingController(
            IBookingService bookingService,
            IAuthenticationService authenticationService,
            IJsonFieldsSerializer jsonFieldsSerializer)
            : base(authenticationService, jsonFieldsSerializer)
        {
            _bookingService = bookingService;
        }

        #region GET

        [HttpGet]
        public async Task<IActionResult> GetAllBookings()
        {
            var result = await _bookingService.GetBookingsByFilterAsync(null);
            return new RawJsonActionResult(
                _jsonFieldsSerializer.Serialize(
                    new ApiResponse(true, "", StatusCodes.Status200OK, result),
                    string.Empty));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBookingsByFilter([FromQuery] BookingFilter filter)
        {
            var result = await _bookingService.GetBookingsByFilterAsync(filter);
            return new RawJsonActionResult(
                _jsonFieldsSerializer.Serialize(
                    new ApiResponse(true, "", StatusCodes.Status200OK, result),
                    string.Empty));
        }

        [HttpGet]
        public async Task<IActionResult> GetBookingById([FromQuery]BaseDto<int> dto)
        {
            var result = await _bookingService.GetBookingByIdAsync(dto);
            return new RawJsonActionResult(
                _jsonFieldsSerializer.Serialize(
                    new ApiResponse(true, "", StatusCodes.Status200OK, result),
                    string.Empty));
        }

        #endregion

        #region POST

        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromBody] CreateBookingDto dto)
        {
            var result = await _bookingService.CreateBookingAsync(dto,await _authenticationService.GetAuthenticatedUser());
            return new RawJsonActionResult(
                _jsonFieldsSerializer.Serialize(
                    new ApiResponse(true, "Booking created successfully", StatusCodes.Status200OK, result),
                    string.Empty));
        }

        #endregion

        #region PUT

        [HttpPut]
        public async Task<IActionResult> UpdateBooking([FromBody] UpdateBookingDto dto)
        {
            var result = await _bookingService.UpdateBookingAsync(dto);
            return new RawJsonActionResult(
                _jsonFieldsSerializer.Serialize(
                    new ApiResponse(true, "Booking updated successfully", StatusCodes.Status200OK, result),
                    string.Empty));
        }

        #endregion

        #region DELETE

        [HttpDelete]
        public async Task<IActionResult> DeleteBookingById([FromQuery] BaseDto<int> dto)
        {
            await _bookingService.DeleteBookingByIdAsync(dto);
            return new RawJsonActionResult(
                _jsonFieldsSerializer.Serialize(
                    new ApiResponse(true, "Booking deleted successfully", StatusCodes.Status200OK),
                    string.Empty));
        }

        #endregion

        [HttpPatch]
        [ProducesResponseType(typeof(ApiResponse<BookingDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ConfirmBooking(int bookingId)
        {
            var currentUser = await GetCurrentUserAsync();
            if(currentUser.Token!.UserRoles.Contains(ApiConsts.EmployeeRoleName) || currentUser.Token!.UserRoles.Contains(ApiConsts.AdminRoleName))
                throw new UnauthorizedAccessException("User is not an employee.");

            var result = await _bookingService.ConfirmBookingAsync(bookingId, currentUser.Id);

            return new RawJsonActionResult(
                _jsonFieldsSerializer.Serialize(
                    new ApiResponse(true, "Booking confirmed successfully.", StatusCodes.Status200OK, result),
                    string.Empty));
        }
    }
}