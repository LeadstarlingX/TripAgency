using Application.DTOs.Car;
using Application.DTOs.Common;
using Application.IApplicationServices.Car;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICarService _carservice;

        public CarController(ICarService carService)
        {
               _carservice = carService;
        }


        [HttpGet]

        public async Task<IActionResult> GetAllCars()
        {
          
               var c=  await _carservice.GetCarsAsync();

            return Ok(c);
        }
        [HttpGet]

        public async Task<IActionResult> GetCarById(BaseDto<int> dto)
        {
          var result=  await _carservice.GetCarByIdAsync(dto);

           return Ok(result);
        }

        [HttpPut]

        public async Task<IActionResult> UpdateCar(UpdateCarDto dto) 
        {

            await _carservice.UpdateCarAsync(dto);
            return Ok(dto);
        
        }
        [HttpPost]
        public async Task<IActionResult> CreateCar(CreateCarDto dto)
        {
            await _carservice.CreateCarAsync(dto);
            return Ok(dto);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCar(BaseDto<int> dto)
        {

            await _carservice.DeleteCarAsync(dto);
            return Ok(dto);
        }


        [HttpGet]
        public async Task<IActionResult> GetCarsByCategory(string category)
        {
            await _carservice.GetCarsByCategory(category);
            return Ok(category);
        }

    }
}
