using Microsoft.AspNetCore.Mvc;
using WebAPIServices.Dto.Category;
using Microsoft.AspNetCore.OutputCaching;
using WebAPIServices.Services.CategoryServices;
using Microsoft.AspNetCore.Authorization;

namespace WebAPIServices.Controllers
{
    [Route("api/Category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryrService _categoryService;

        public CategoryController(ICategoryrService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoryDto>>> GetAllCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetSingleCategory(int id)
        {
            var category = await _categoryService.GetSingleCategoryAsync(id);
            if (category == null)
            {
                return NotFound("Category not found");
            }
            return Ok(category);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<CategoryDto>> AddCategory(CreateCategoryDto categoryDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var addedCategory = await _categoryService.AddCategoryAsync(categoryDto);
            return Ok(addedCategory);

        }
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<CategoryDto>> UpdateCategory(int id, UpdateCategoryDto categoryDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedCategory = await _categoryService.UpdateCategoryAsync(id, categoryDto);
            if (updatedCategory == null)
            {
                return NotFound("Category not found");
            }

            return Ok(updatedCategory);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<CategoryDto>>> DeleteCategory(int id)
        {
            var deletedCategories = await _categoryService.DeleteCategoryAsync(id);
            if (deletedCategories == null)
            {
                return NotFound("Category not found");
            }
            return Ok(deletedCategories);
        }
    }
}
