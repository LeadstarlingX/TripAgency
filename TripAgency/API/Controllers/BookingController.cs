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

<<<<<<< HEAD
        #region Get
        /// <summary>
        /// Gets all bookings.
        /// </summary>
        /// <returns></returns>
        //[Authorize]
=======
        #region GET

>>>>>>> AlaaWork
        [HttpGet]
        public async Task<IActionResult> GetAllBookings()
        {
<<<<<<< HEAD
            var result = await _bookingService.GetByFilterAsync(null);

            if (result == null)
                return new RawJsonActionResult(
                    _jsonFieldsSerializer.Serialize(
                        new ApiResponse(true, "There are no bookings", StatusCodes.Status200OK),
                        string.Empty));

            return new RawJsonActionResult(
                    _jsonFieldsSerializer.Serialize(
                        new ApiResponse(true, "", StatusCodes.Status200OK, result),
                        string.Empty));
            
        }


        /// <summary>
        /// Gets all bookings by filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        //[Authorize]
=======
            var result = await _bookingService.GetBookingsByFilterAsync(null);
            return new RawJsonActionResult(
                _jsonFieldsSerializer.Serialize(
                    new ApiResponse(true, "", StatusCodes.Status200OK, result),
                    string.Empty));
        }

>>>>>>> AlaaWork
        [HttpGet]
        public async Task<IActionResult> GetAllBookingsByFilter([FromQuery] BookingFilter filter)
        {
<<<<<<< HEAD

            var result = await _bookingService.GetByFilterAsync(filter);

            if (result == null)
                return new RawJsonActionResult(
                    _jsonFieldsSerializer.Serialize(
                        new ApiResponse(true, "There are no bookings", StatusCodes.Status200OK),
                        string.Empty));

=======
            var result = await _bookingService.GetBookingsByFilterAsync(filter);
>>>>>>> AlaaWork
            return new RawJsonActionResult(
                _jsonFieldsSerializer.Serialize(
                    new ApiResponse(true, "", StatusCodes.Status200OK, result),
                    string.Empty));
        }

<<<<<<< HEAD

        /// <summary>
        /// Gets the booking by identifier.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <returns></returns>
        //[Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<List<BookingDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByIdAsync([FromQuery] BaseDto<int> dto)
        {
            try
            {
                var result = await _bookingService.GetByIdAsync(dto);

                return new RawJsonActionResult(
                        _jsonFieldsSerializer.Serialize(
                            new ApiResponse(true, "", StatusCodes.Status200OK, result),
                            string.Empty));

            }

            catch (Exception)
            {
                return new RawJsonActionResult(
                    _jsonFieldsSerializer.Serialize(
                        new ApiResponse(false, "There are no such booking", StatusCodes.Status400BadRequest),
                        string.Empty));
            }
=======
        [HttpGet]
        public async Task<IActionResult> GetBookingById([FromQuery]BaseDto<int> dto)
        {
            var result = await _bookingService.GetBookingByIdAsync(dto);
            return new RawJsonActionResult(
                _jsonFieldsSerializer.Serialize(
                    new ApiResponse(true, "", StatusCodes.Status200OK, result),
                    string.Empty));
>>>>>>> AlaaWork
        }

        #endregion

<<<<<<< HEAD
        /// <summary>
        /// Updates the booking.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <returns></returns>
        //[Authorize]
        [HttpPut]
        [ProducesResponseType(typeof(ApiResponse<List<BookingDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateBooking([FromQuery] UpdateBookingDto dto)
        {
            var result = await _bookingService.UpdateAsync(dto);

            return new RawJsonActionResult(
                _jsonFieldsSerializer.Serialize(
                    new ApiResponse(true, "", StatusCodes.Status200OK, result),
                    string.Empty));
        }


        /// <summary>
        /// Creates the booking.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <returns></returns>
        //[Authorize]
=======
        #region POST

>>>>>>> AlaaWork
        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromBody] CreateBookingDto dto)
        {
<<<<<<< HEAD
            await _bookingService.CreateAsync(dto);

=======
            var result = await _bookingService.CreateBookingAsync(dto,await _authenticationService.GetAuthenticatedUser());
>>>>>>> AlaaWork
            return new RawJsonActionResult(
                _jsonFieldsSerializer.Serialize(
                    new ApiResponse(true, "Booking created successfully", StatusCodes.Status200OK, result),
                    string.Empty));
        }

<<<<<<< HEAD
        /// <summary>
        /// Deletes the booking by identifier.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <returns></returns>
        //[Authorize]
        [HttpDelete]
        [ProducesResponseType(typeof(ApiResponse<List<BookingDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteBookingById([FromQuery] BaseDto<int> dto)
        {

            _bookingService.DeleteByIdAsync(dto);
            return new RawJsonActionResult(
                _jsonFieldsSerializer.Serialize(
                    new ApiResponse(true, "", StatusCodes.Status200OK),
                    string.Empty));

=======
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
>>>>>>> AlaaWork
        }
    }
}