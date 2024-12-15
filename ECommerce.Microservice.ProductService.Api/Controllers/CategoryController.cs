using ECommerce.Microservice.ProductService.Api.Models.Category;
using ECommerce.Microservice.ProductService.Api.Services;
using ECommerce.Microservice.SharedLibrary.Response;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Microservice.ProductService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("getAllCategories")]
        public async Task<IActionResult> GetAllCategories()
        {
            var response = await _categoryService.GetAllCategories();

            if (response != null && response.responseResult == ResponseResultEnum.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpGet("getCategoryById")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var response = await _categoryService.GetCategoryById(id);

            if (response != null && response.responseResult == ResponseResultEnum.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpPost("createNewCategory")]
        public async Task<IActionResult> CreateNewCategory(CategoryModel model)
        {
            var response = await _categoryService.CreateNewCategory(model);

            if (response != null && response.responseResult == ResponseResultEnum.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpPost("updateCategory")]
        public async Task<IActionResult> UpdateCategory(CategoryModel model)
        {
            var response = await _categoryService.UpdateCategory(model);

            if (response != null && response.responseResult == ResponseResultEnum.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpPost("deleteCategory")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var response = await _categoryService.DeleteCategory(id);

            if (response != null && response.responseResult == ResponseResultEnum.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }
    }
}
