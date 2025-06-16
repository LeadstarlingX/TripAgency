using Application.Common;
using Application.DTOs.Car;
using Application.DTOs.Customer;
using Application.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IApplicationServices.Car
{
    public  interface ICarService
    {

        Task<IEnumerable<CarDto>> GetCarsAsync();
        Task<CarDto> CreateCarAsync(CreateCarDto createCarDto);
        Task<CarDto> UpdateCarAsync(UpdateCarDto updatecarDto);
        Task<CarDto> DeleteCarAsync(BaseDto<int>  dto);
        Task<CarDto> GetCarByIdAsync(BaseDto<int> id);
        Task<IEnumerable<CarDto>> GetCarByColor(string color);

        Task<IEnumerable<CarDto>> GetCarsByCategory(string category);

        Task<IEnumerable<CarDto>> FilterCar(CarFilter filter);
    }
}
