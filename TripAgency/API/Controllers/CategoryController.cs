using Application.DTOs.Category;
using Application.DTOs.Common;
using Infrastructure.ApplicationServices.Category;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService _categoryService;

        public CategoryController(CategoryService categoryService)
        {
             _categoryService = categoryService;

        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategory()
        { 
              var c = await _categoryService.GetCategoriesAsync();
            return Ok(c);        
        }



        [HttpGet]


        public async Task<IActionResult> GetCategoryById(BaseDto<int> dto)
        {
            var category = await _categoryService.GetCategoryByIdAsync(dto);
            return Ok(category);
        }

        [HttpPost]

        public async Task<IActionResult> Insertcategory(CreateCategoryDto createCategoryDto)
        {
            await _categoryService.CreateCategoryAsync(createCategoryDto);
            return Ok();
        }

        [HttpPut]

        public async Task<IActionResult> UPdateCategory(UpdateCategoryDto updateCategoryDto)
        {
            await _categoryService.UpdateCategoryAsync(updateCategoryDto);
            return Ok();

        }

        [HttpDelete]

        public async Task<IActionResult> DeleteCategory(BaseDto<int> dto)
        {
            await _categoryService.DeleteCategoryAsync(dto);
            return Ok();
        }
    }
}
