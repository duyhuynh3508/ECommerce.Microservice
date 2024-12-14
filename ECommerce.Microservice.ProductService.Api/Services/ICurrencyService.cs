using ECommerce.Microservice.ProductService.Api.Entities;
using ECommerce.Microservice.ProductService.Api.Mapping;
using ECommerce.Microservice.ProductService.Api.Models.Currency;
using ECommerce.Microservice.ProductService.Api.Repositories;
using ECommerce.Microservice.SharedLibrary.BaseEntity;
using ECommerce.Microservice.SharedLibrary.BaseModel;
using ECommerce.Microservice.SharedLibrary.Mapping;
using ECommerce.Microservice.SharedLibrary.Response;

namespace ECommerce.Microservice.ProductService.Api.Services
{
    public interface ICurrencyService
    {
        Task<ResponseResult> GetCurrencyById(int id);
        Task<ResponseResult> GetAllCurrencies();
        Task<ResponseResult> CreateNewCurrency(CurrencyModel model);
        Task<ResponseResult> UpdateCurrency(CurrencyModel model);
        Task<ResponseResult> DeleteCurrency(int id);
    }

    public class CurrencyService : ICurrencyService
    {
        private readonly ICurrencyRepository _currencyRepository;
        private readonly ICurrencyMapping _currencyMapping;

        public CurrencyService(ICurrencyRepository currencyRepository, ICurrencyMapping currencyMapping)
        {
            _currencyRepository = currencyRepository;
            _currencyMapping = currencyMapping;
        }

        public async Task<ResponseResult> CreateNewCurrency(CurrencyModel model)
        {
            var currency = (Currency)_currencyMapping.ToEntity(model);

            var responseResult = await _currencyRepository.CreateAsync(currency);

            return responseResult;
        }

        public async Task<ResponseResult> DeleteCurrency(int id)
        {
            var responseResult = await _currencyRepository.DeleteAsync(id);

            return responseResult;
        }

        public async Task<ResponseResult> GetAllCurrencies()
        {
            var currencies = await _currencyRepository.GetAllAsync();
            List<CurrencyModel> currencyModels = new List<CurrencyModel>();

            if (currencies == null || !currencies.Any())
                return new ResponseResult(ResponseResultEnum.Error, "Currencies are empty");

            foreach (var item in currencies)
            {
                var model = (CurrencyModel)_currencyMapping.ToModel(item);
                currencyModels.Add(model);
            }

            return new ResponseResult(ResponseResultEnum.Success, "", null, currencyModels);
        }

        public async Task<ResponseResult> GetCurrencyById(int id)
        {
            var currency = await _currencyRepository.GetByIdAsync(id);

            if (currency == null) return new ResponseResult(ResponseResultEnum.Error, "Cannot find currency!");

            var currencyModel = (CurrencyModel)_currencyMapping.ToModel(currency);

            return new ResponseResult(ResponseResultEnum.Success, "", currencyModel);
        }

        public async Task<ResponseResult> UpdateCurrency(CurrencyModel model)
        {
            if (model == null)
                return new ResponseResult(ResponseResultEnum.Error, "Cannot find currency!");

            var currencyToUpdate = await _currencyRepository.GetByIdAsync(model.CurrencyID);

            if (currencyToUpdate == null) return new ResponseResult(ResponseResultEnum.Error, "Cannot find currency!");

            currencyToUpdate = (Currency)_currencyMapping.ToEntity(currencyToUpdate, model);

            var responseResult = await _currencyRepository.UpdateAsync(currencyToUpdate);

            return responseResult;
        }
    }
}
