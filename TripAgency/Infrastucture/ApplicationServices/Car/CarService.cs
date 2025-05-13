using Application.DTOs.Car;
using Application.DTOs.Common;
using Application.Filter;
using Application.IApplicationServices.Car;
using Application.IReositosy;
using AutoMapper;
using Domain.Entities.ApplicationEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ApplicationServices
{
    public class CarService : ICarService
    {
        private readonly IAppRepository<Car> _carRepositry;
       // private readonly IAppRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;

        public CarService(IAppRepository<Car> carRepository ,IMapper mapper)
        {
             _carRepositry = carRepository;
            _mapper = mapper;
           // _categoryRepository = categoryRpository;
        }
        public async Task<CarDto> CreateCarAsync(CreateCarDto createCarDto)
        {
            var car =   _mapper.Map<Car>(createCarDto);
            await _carRepositry.InsertAsync(car);
            return _mapper.Map<CarDto>(car);
        }


        public async Task DeleteCarAsync(BaseDto<int> dto)
        {

            var car = (await _carRepositry.FindAsync(x => x.Id == dto.Id)).FirstOrDefault();
            if(car==null)
            {
                throw new KeyNotFoundException("car Not found");
            }

              await _carRepositry.RemoveAsync(car);

        }  

        public async Task<IEnumerable<CarDto>> GetCarByColor(string color)
        {
            //    var car = await GetCarsAsync();

            //    var result =car.Where(c=>c.Color == color).FirstOrDefault();
            //return _mapper.Map<CarDto>(result);
            var car = await _carRepositry.FindAsync(x => x.Color == color);
            
            return _mapper.Map<IEnumerable<CarDto>>(car);

        }

        public async Task<CarDto> GetCarByIdAsync(BaseDto<int> dto)
        {
            var car = (await _carRepositry.FindAsync(x => x.Id == dto.Id)).FirstOrDefault();
            if (car==null)
            {
                throw new KeyNotFoundException("Car Not Found");

            }

            return _mapper.Map<CarDto>(car);
        }

        public async Task<IEnumerable<CarDto>> GetCarsAsync()
        {

            var c = await _carRepositry.GetAllWithAllIncludeAsync();
            return _mapper.Map<IEnumerable<CarDto>>(c);
        }

        public async Task<CarDto> UpdateCarAsync(UpdateCarDto updatecarDto)
        {
            var c = await _carRepositry.UpdateAsync(_mapper.Map<Car>(updatecarDto));
            return _mapper.Map<CarDto>(c);
        }
        public async Task<IEnumerable<CarDto>> GetCarsByCategory(string category)
        {
            //    var c = await _categoryRepository.GetAllWithAllIncludeAsync();
            //    var result = c.Where(x => x.Title == category).FirstOrDefault();

            //    IEnumerable<Car> cars = result.Cars.ToList();

            //    return _mapper.Map<IEnumerable<CarDto>>(cars);

            var result = await _carRepositry.FindAsync(x => x.Category.Title == category, x=> x.Category);

            if (result==null)
            {
                throw new KeyNotFoundException("not Found car ");
            }
            var c=  _mapper.Map<IEnumerable<CarDto>>(result);

            return c;


        }

           public  async  Task<IEnumerable<CarDto>> FilterCar(CarFilter filter)
        {
            var query = await _carRepositry.GetAllAsync();

            if(filter.Capacity != null)
            {
                query = query.Where(c => c.Capacity == filter.Capacity);
            }

            if(filter.Color != null)
            {
                query =query.Where(c => c.Color==filter.Color);
            }
            if(filter.Model != null)
            {
                query =query.Where(c=>c.Model ==filter.Model);
            }
            if(filter.Mbw != null)
            {
                query =query.Where(c=>c.Mbw == filter.Mbw);

            }
            if(filter.Ppd != null)
            {
                query =query.Where(x=>x.Ppd == filter.Ppd);
            }
            if (filter.Pph != null) { 
            query =query.Where(x=>x.Pph==filter.Pph);
            
            }
            return _mapper.Map<IEnumerable<CarDto>>(query);


        }
    }
}
