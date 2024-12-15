using ECommerce.Microservice.ProductService.Api.Models.Product;
using ECommerce.Microservice.ProductService.Api.Services;
using ECommerce.Microservice.SharedLibrary.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Microservice.ProductService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [Authorize]
        [HttpGet("getAllProducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            var response = await _productService.GetAllProducts();

            if (response != null && response.responseResult == ResponseResultEnum.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [Authorize]
        [HttpGet("getProductById")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var response = await _productService.GetProductById(id);

            if (response != null && response.responseResult == ResponseResultEnum.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [Authorize]
        [HttpGet("getProductsByIds")]
        public async Task<IActionResult> GetProductsById(IEnumerable<int> ids)
        {
            var response = await _productService.GetProductsByIds(ids);

            if (response != null && response.responseResult == ResponseResultEnum.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [Authorize]
        [HttpGet("getProductsByCategoryIds")]
        public async Task<IActionResult> GetProductsByCategoryIds(IEnumerable<int> categoryIds)
        {
            var response = await _productService.GetProductsByCategoryIds(categoryIds);

            if (response != null && response.responseResult == ResponseResultEnum.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [Authorize]
        [HttpGet("getProductsByCategoryId")]
        public async Task<IActionResult> GetProductsByCategoryId(int categoryId)
        {
            var response = await _productService.GetProductsByCategoryId(categoryId);

            if (response != null && response.responseResult == ResponseResultEnum.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [Authorize]
        [HttpPost("createNewProduct")]
        public async Task<IActionResult> CreateNewProduct(ProductCreateModel model)
        {
            var response = await _productService.CreateNewProduct(model);

            if (response != null && response.responseResult == ResponseResultEnum.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpPost("updateProduct")]
        public async Task<IActionResult> UpdateProduct(ProductUpdateModel model)
        {
            var response = await _productService.UpdateProduct(model);

            if (response != null && response.responseResult == ResponseResultEnum.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpPost("deleteProduct")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var response = await _productService.DeleteProduct(id);

            if (response != null && response.responseResult == ResponseResultEnum.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }
    }
}
