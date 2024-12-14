using ECommerce.Microservice.ProductService.Api.Models.Currency;
using ECommerce.Microservice.ProductService.Api.Services;
using ECommerce.Microservice.SharedLibrary.Response;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Microservice.ProductService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyService _currencyService;

        public CurrencyController(ICurrencyService currencyService)
        {
            _currencyService = currencyService;
        }

        [HttpGet("getAllCurrencies")]
        public async Task<IActionResult> GetAllCurrencies()
        {
            var response = await _currencyService.GetAllCurrencies();

            if (response != null && response.responseResult == ResponseResultEnum.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpGet("getCurrencyById/{id}")]
        public async Task<IActionResult> GetCurrencyById(int id)
        {
            var response = await _currencyService.GetCurrencyById(id);

            if (response != null && response.responseResult == ResponseResultEnum.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpPost("createNewCurrency")]
        public async Task<IActionResult> CreateNewCurrency(CurrencyModel model)
        {
            var response = await _currencyService.CreateNewCurrency(model);

            if (response != null && response.responseResult == ResponseResultEnum.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpPost("updateCurrency")]
        public async Task<IActionResult> UpdateCurrency(CurrencyModel model)
        {
            var response = await _currencyService.UpdateCurrency(model);

            if (response != null && response.responseResult == ResponseResultEnum.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpPost("deleteCurrency/{id}")]
        public async Task<IActionResult> DeleteCurrency(int id)
        {
            var response = await _currencyService.DeleteCurrency(id);

            if (response != null && response.responseResult == ResponseResultEnum.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }
    }
}
