using ECommerce.Microservice.ProductService.Api.Models.Product;
using ECommerce.Microservice.ProductService.Api.Services;
using ECommerce.Microservice.SharedLibrary.Response;
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

        [HttpGet("getProductById/{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var response = await _productService.GetProductById(id);

            if (response != null && response.responseResult == ResponseResultEnum.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpGet("getProductsByIds/{ids}")]
        public async Task<IActionResult> GetProductById(IEnumerable<int> ids)
        {
            var response = await _productService.GetProductsByIds(ids);

            if (response != null && response.responseResult == ResponseResultEnum.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

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

        [HttpPost("deleteProduct/{id}")]
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
