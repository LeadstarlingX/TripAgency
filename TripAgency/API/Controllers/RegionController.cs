using Application.Common;
using Application.DTOs.Actions;
using Application.DTOs.Region;
using Application.IApplicationServices.Authentication;
using Application.IApplicationServices.Region;
using Application.Serializer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    [ApiController]
    public class RegionController : BaseAuthenticatedController
    {
        private readonly IRegionService _regionService;
        

        public RegionController(IAuthenticationService authenticationService,
            IJsonFieldsSerializer jsonFieldsSerializer,
            IRegionService regionService) : base(authenticationService, jsonFieldsSerializer)
        {
            _regionService = regionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetRegionById([FromQuery] BaseDto<int> dto)
        {
            var result = await _regionService.GetRegionByIdAsync(dto);
            return new RawJsonActionResult(
                _jsonFieldsSerializer.Serialize(
                    new ApiResponse(true, "Region retrieved successfully", StatusCodes.Status200OK, result),
                    string.Empty));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRegions()
        {
            var result = await _regionService.GetAllRegionsAsync();
            return new RawJsonActionResult(
                _jsonFieldsSerializer.Serialize(
                    new ApiResponse(true, "All regions retrieved successfully", StatusCodes.Status200OK, result),
                    string.Empty));
        }

        [HttpPost]
        public async Task<IActionResult> CreateRegion([FromBody] CreateRegionDto dto)
        {
            if (!ModelState.IsValid)
            {
                return new RawJsonActionResult(
                    _jsonFieldsSerializer.Serialize(
                        new ApiResponse(false, "Invalid request data", StatusCodes.Status400BadRequest),
                        string.Empty));
            }

            var result = await _regionService.CreateRegionAsync(dto);
            return new RawJsonActionResult(
                _jsonFieldsSerializer.Serialize(
                    new ApiResponse(true, "Region created successfully", StatusCodes.Status200OK, result),
                    string.Empty));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRegion([FromBody] UpdateRegionDto dto)
        {
            if (!ModelState.IsValid)
            {
                return new RawJsonActionResult(
                    _jsonFieldsSerializer.Serialize(
                        new ApiResponse(false, "Invalid request data", StatusCodes.Status400BadRequest),
                        string.Empty));
            }

            var result = await _regionService.UpdateRegionAsync(dto);
            return new RawJsonActionResult(
                _jsonFieldsSerializer.Serialize(
                    new ApiResponse(true, "Region updated successfully", StatusCodes.Status200OK, result),
                    string.Empty));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRegion([FromQuery] BaseDto<int> dto)
        {
            await _regionService.DeleteRegionAsync(dto);
            return new RawJsonActionResult(
                _jsonFieldsSerializer.Serialize(
                    new ApiResponse(true, "Region deleted successfully", StatusCodes.Status200OK),
                    string.Empty));
        }
    }
}
