using ECommerce.Microservice.ProductService.Api.Entities;
using ECommerce.Microservice.ProductService.Api.Mapping;
using ECommerce.Microservice.ProductService.Api.Models.Currency;
using ECommerce.Microservice.ProductService.Api.RedisCaching;
using ECommerce.Microservice.ProductService.Api.Repositories;
using ECommerce.Microservice.SharedLibrary.Logging;
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
        private readonly ICurrencyRepository _repository;
        private readonly ICurrencyMapping _mapping;
        private readonly IRedisCachingHandler _cacheHelper;
        private const string CurrencyCacheKey = "currencies";
        public CurrencyService(ICurrencyRepository repository, ICurrencyMapping mapping, IRedisCachingHandler cacheHelper)
        {
            _repository = repository;
            _mapping = mapping;
            _cacheHelper = cacheHelper;
        }

        public async Task<ResponseResult> CreateNewCurrency(CurrencyModel model)
        {
            var currency = (Currency)_mapping.ToEntity(model);

            var responseResult = await _repository.CreateAsync(currency);

            try
            {
                if (responseResult != null && responseResult.responseResult == ResponseResultEnum.Success)
                    await _cacheHelper.RemoveAsync(CurrencyCacheKey);
            }
            catch (Exception ex)
            {
                LoggingService.LogException(ex);
                return responseResult;
            }

            return responseResult;
        }

        public async Task<ResponseResult> DeleteCurrency(int id)
        {
            var responseResult = await _repository.DeleteAsync(id);

            return responseResult;
        }

        public async Task<ResponseResult> GetAllCurrencies()
        {
            var currencies = await _repository.GetAllAsync();
            List<CurrencyModel> currencyModels = new List<CurrencyModel>();

            if (currencies == null || !currencies.Any())
                return new ResponseResult(ResponseResultEnum.Error, "Currencies are empty");

            foreach (var item in currencies)
            {
                var model = (CurrencyModel)_mapping.ToModel(item);
                currencyModels.Add(model);
            }

            return new ResponseResult(ResponseResultEnum.Success, "", null, currencyModels);
        }

        public async Task<ResponseResult> GetCurrencyById(int id)
        {
            var currency = await _repository.GetByIdAsync(id);

            if (currency == null) return new ResponseResult(ResponseResultEnum.Error, "Cannot find currency!");

            var currencyModel = (CurrencyModel)_mapping.ToModel(currency);

            return new ResponseResult(ResponseResultEnum.Success, "", currencyModel);
        }

        public async Task<ResponseResult> UpdateCurrency(CurrencyModel model)
        {
            if (model == null)
                return new ResponseResult(ResponseResultEnum.Error, "Cannot find currency!");

            var currencyToUpdate = await _repository.GetByIdAsync(model.CurrencyID);

            if (currencyToUpdate == null) return new ResponseResult(ResponseResultEnum.Error, "Cannot find currency!");

            currencyToUpdate = (Currency)_mapping.ToEntity(currencyToUpdate, model);

            var responseResult = await _repository.UpdateAsync(currencyToUpdate);

            return responseResult;
        }
    }
}
