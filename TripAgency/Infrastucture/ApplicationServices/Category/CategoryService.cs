using Application.Common;
using Application.DTOs.Car;
using Application.DTOs.Category;
using Application.IApplicationServices.Category;
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
    public class CategoryService : ICategoryService
    {

        private readonly IAppRepository<Category> _categoryRepository;
        private readonly IAppRepository<Car> _carRepository;
        private readonly  IMapper _mapper ;



        public CategoryService(IAppRepository<Category> cat ,IMapper mapper,IAppRepository<Car> carRepository)
        { 
            _categoryRepository = cat;
            _mapper = mapper;
            _carRepository = carRepository;
            
        }




        public async Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto createCategoryDto)
        {
           var c=  _mapper.Map<Category>(createCategoryDto);
            await _categoryRepository.InsertAsync(c);

            return _mapper.Map<CategoryDto>(c);
           
        }

        public async Task<CategoryDto> DeleteCategoryAsync(BaseDto<int> dto)
        {
            var c = (await _categoryRepository.FindAsync(x=> x.Id == dto.Id)).FirstOrDefault();
            if (c == null)
            {
                throw new KeyNotFoundException("category not found");
            }
            await _categoryRepository.RemoveAsync(c);
            return _mapper.Map<CategoryDto>(c);
        }

        public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync()
        {
            var c= await _categoryRepository.GetAllWithAllIncludeAsync();

            return _mapper.Map<IEnumerable<CategoryDto>>(c);
        }

        public async Task<CategoryDto> GetCategoryByIdAsync(BaseDto<int> Dto)
        {
            var c = (await _categoryRepository.FindAsync(x => x.Id == Dto.Id)).FirstOrDefault();
            return _mapper.Map<CategoryDto>(c);
        }

        public async Task<CategoryDto> UpdateCategoryAsync(UpdateCategoryDto updatecategoryDto)
        {
            var c= _mapper.Map<Category>(updatecategoryDto);
            await _categoryRepository.UpdateAsync(c);
            return _mapper.Map<CategoryDto>(c);
        }

     
    }
}
