using Application.Common;
using Application.DTOs.Actions;
using Application.DTOs.Car;
using Application.DTOs.Tag;
using Application.Filter;
using Application.IApplicationServices.Authentication;
using Application.IApplicationServices.Car;
using Application.Serializer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CarController : BaseAuthenticatedController
    {
        private readonly ICarService _carService;
        private readonly IWebHostEnvironment _webHostEnvironment; // Inject environment

        public CarController(ICarService carService, IWebHostEnvironment webHostEnvironment, IJsonFieldsSerializer jsonFieldsSerializer, IAuthenticationService authenticationService) :base(authenticationService,jsonFieldsSerializer)
        {
            _carService = carService;
            _webHostEnvironment = webHostEnvironment; // Initialize
        }
       

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<List<CarDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllCars()
        {
            var cars = await _carService.GetCarsAsync();
            return new RawJsonActionResult(
                _jsonFieldsSerializer.Serialize(
                    new ApiResponse(true, "", StatusCodes.Status200OK, cars),
                    string.Empty));
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<List<CarDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> FilterCars([FromQuery] CarFilter filter)
        {
            var result = await _carService.FilterCar(filter);
            return new RawJsonActionResult(
                _jsonFieldsSerializer.Serialize(
                    new ApiResponse(true, "", StatusCodes.Status200OK, result),
                    string.Empty));
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<CarDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCarById([FromQuery]BaseDto<int> dto)
        {
            var car = await _carService.GetCarByIdAsync(dto);
            if (car == null)
            {
                return new RawJsonActionResult(
                    _jsonFieldsSerializer.Serialize(
                        new ApiResponse(false, "Car not found", StatusCodes.Status404NotFound),
                        string.Empty));
            }

            return new RawJsonActionResult(
                _jsonFieldsSerializer.Serialize(
                    new ApiResponse(true, "", StatusCodes.Status200OK, car),
                    string.Empty));
        }

        [HttpPut]
        [ProducesResponseType(typeof(ApiResponse<CarDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateCar(UpdateCarDto dto)
        {
            var result = await _carService.UpdateCarAsync(dto);
           
            return new RawJsonActionResult(
                _jsonFieldsSerializer.Serialize(
                    new ApiResponse(true, "Car updated successfully", StatusCodes.Status200OK, result),
                    string.Empty));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<CarDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateCar([FromForm]CreateCarDto dto)
        {
            if (dto.ImageFile != null)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(dto.ImageFile.FileName);
                string directoryPath = Path.Combine(wwwRootPath, "images", "cars");

                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                string filePath = Path.Combine(directoryPath, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await dto.ImageFile.CopyToAsync(fileStream);
                }

                dto.Image = "/images/cars/" + fileName;
            }
            var result = await _carService.CreateCarAsync(dto);
            if (result == null)
            {
                return new RawJsonActionResult(
                    _jsonFieldsSerializer.Serialize(
                        new ApiResponse(false, "failed create", StatusCodes.Status400BadRequest),
                        string.Empty));
            }

            return new RawJsonActionResult(
                _jsonFieldsSerializer.Serialize(
                    new ApiResponse(true, "Car created successfully", StatusCodes.Status201Created, result),
                    string.Empty));
        }

        [HttpDelete]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteCar(BaseDto<int> dto)
        {
            var result = await _carService.DeleteCarAsync(dto);
            if (result == null)
            {
                return new RawJsonActionResult(
                    _jsonFieldsSerializer.Serialize(
                        new ApiResponse(false, "failed", StatusCodes.Status400BadRequest),
                        string.Empty));
            }

            return new RawJsonActionResult(
                _jsonFieldsSerializer.Serialize(
                    new ApiResponse(true, "Car deleted successfully", StatusCodes.Status200OK),
                    string.Empty));
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<List<CarDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCarsByCategory(string category)
        {
            var result = await _carService.GetCarsByCategory(category);
            if (result == null || !result.Any())
            {
                return new RawJsonActionResult(
                    _jsonFieldsSerializer.Serialize(
                        new ApiResponse(false, "No cars found for this category", StatusCodes.Status404NotFound),
                        string.Empty));
            }

            return new RawJsonActionResult(
                _jsonFieldsSerializer.Serialize(
                    new ApiResponse(true, "", StatusCodes.Status200OK, result),
                    string.Empty));
        }
    }
}