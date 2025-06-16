//using Application.Common;
//using Application.DTOs.Actions;
//using Application.DTOs.Trip;
//using Application.IApplicationServices.Authentication;
//using Application.IApplicationServices.Trip;
//using Application.Serializer;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace API.Controllers
//{
//    public class TripController : BaseAuthenticatedController
//    {
//        private readonly ITripService _tripService;

//        public TripController(
//            ITripService tripService,
//            IAuthenticationService authenticationService,
//            IJsonFieldsSerializer jsonFieldsSerializer)
//            : base(authenticationService, jsonFieldsSerializer)
//        {
//            _tripService = tripService;
//        }

//        [HttpGet]
//        public async Task<IActionResult> GetTripsByFilter([FromQuery] TripFilter filter)
//        {
//            var result = await _tripService.GetTripsByFilterAsync(filter);
//            return new RawJsonActionResult(
//                _jsonFieldsSerializer.Serialize(
//                    new ApiResponse(true, "Filtered trips retrieved successfully", StatusCodes.Status200OK, result),
//                    string.Empty));
//        }

//        [HttpGet]
//        public async Task<IActionResult> GetTripById([FromQuery] BaseDto<int> dto)
//        {
//            var result = await _tripService.GetTripByIdAsync(dto);
//            return new RawJsonActionResult(
//                _jsonFieldsSerializer.Serialize(
//                    new ApiResponse(true, "Trip retrieved successfully", StatusCodes.Status200OK, result),
//                    string.Empty));
//        }

//        [HttpGet]
//        public async Task<IActionResult> GetAllTrips()
//        {
//            var result = await _tripService.GetAllTripsAsync();
//            return new RawJsonActionResult(
//                _jsonFieldsSerializer.Serialize(
//                    new ApiResponse(true, "All trips retrieved successfully", StatusCodes.Status200OK, result),
//                    string.Empty));
//        }

//        [HttpPost]
//        public async Task<IActionResult> CreateTrip([FromBody] CreateTripDto dto)
//        {
//            if (!ModelState.IsValid)
//            {
//                return new RawJsonActionResult(
//                    _jsonFieldsSerializer.Serialize(
//                        new ApiResponse(false, "Invalid request data", StatusCodes.Status400BadRequest),
//                        string.Empty));
//            }

//            var result = await _tripService.CreateTripAsync(dto);
//            return new RawJsonActionResult(
//                _jsonFieldsSerializer.Serialize(
//                    new ApiResponse(true, "Trip created successfully", StatusCodes.Status200OK, result),
//                    string.Empty));
//        }

//        [HttpPut]
//        public async Task<IActionResult> UpdateTrip([FromBody] UpdateTripDto dto)
//        {
//            if (!ModelState.IsValid)
//            {
//                return new RawJsonActionResult(
//                    _jsonFieldsSerializer.Serialize(
//                        new ApiResponse(false, "Invalid request data", StatusCodes.Status400BadRequest),
//                        string.Empty));
//            }

//            var result = await _tripService.UpdateTripAsync(dto);
//            return new RawJsonActionResult(
//                _jsonFieldsSerializer.Serialize(
//                    new ApiResponse(true, "Trip updated successfully", StatusCodes.Status200OK, result),
//                    string.Empty));
//        }

//        [HttpDelete]
//        public async Task<IActionResult> DeleteTrip([FromQuery] BaseDto<int> dto)
//        {
//            await _tripService.DeleteTripAsync(dto);
//            return new RawJsonActionResult(
//                _jsonFieldsSerializer.Serialize(
//                    new ApiResponse(true, "Trip deleted successfully", StatusCodes.Status200OK),
//                    string.Empty));
//        }
//    }
//}
