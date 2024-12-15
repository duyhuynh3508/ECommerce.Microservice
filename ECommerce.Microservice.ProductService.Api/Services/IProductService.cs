using ECommerce.Microservice.ProductService.Api.Entities;
using ECommerce.Microservice.ProductService.Api.Mapping;
using ECommerce.Microservice.ProductService.Api.Models.Product;
using ECommerce.Microservice.ProductService.Api.RedisCaching;
using ECommerce.Microservice.ProductService.Api.Repositories;
using ECommerce.Microservice.SharedLibrary.Response;

namespace ECommerce.Microservice.ProductService.Api.Services
{
    public interface IProductService
    {
        Task<ResponseResult> GetProductById(int id);
        Task<ResponseResult> GetProductsByIds(IEnumerable<int> ids);
        Task<ResponseResult> GetAllProducts();
        Task<ResponseResult> GetProductsByCategoryId(int categoryId);
        Task<ResponseResult> GetProductsByCategoryIds(IEnumerable<int> categoryIds);
        Task<ResponseResult> CreateNewProduct(ProductCreateModel model);
        Task<ResponseResult> UpdateProduct(ProductUpdateModel model);
        Task<ResponseResult> DeleteProduct(int id);
    }
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IProductMapping _mapping;
        private readonly IRedisCachingHandler _cacheHelper;
        private const string CategoryCacheKey = "categories";
        private const string CurrencyCacheKey = "currencies";
        public ProductService(IProductRepository repository,  IProductMapping mapping, 
            ICategoryRepository categoryRepository, ICurrencyRepository currencyRepository, IRedisCachingHandler cacheHelper)
        {
            _repository = repository;
            _mapping = mapping;
            _categoryRepository = categoryRepository;
            _currencyRepository = currencyRepository;
            _cacheHelper = cacheHelper;
        }
        private async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            var cachedCategories = await _cacheHelper.GetAsync<IEnumerable<Category>>(CategoryCacheKey);
            if (cachedCategories == null)
            {
                var categories = await _categoryRepository.GetAllAsync();
                await _cacheHelper.SetAsync(CategoryCacheKey, categories, TimeSpan.FromHours(1));
                return categories;
            }
            return cachedCategories;
        }

        private async Task<IEnumerable<Currency>> GetCurrenciesAsync()
        {
            var cachedCurrencies = await _cacheHelper.GetAsync<IEnumerable<Currency>>(CurrencyCacheKey);
            if (cachedCurrencies == null)
            {
                var currencies = await _currencyRepository.GetAllAsync();
                await _cacheHelper.SetAsync(CurrencyCacheKey, currencies, TimeSpan.FromHours(1));
                return currencies;
            }
            return cachedCurrencies;
        }

        public async Task<ResponseResult> CreateNewProduct(ProductCreateModel model)
        {
            var product = (Product)_mapping.ToEntity(model);

            var responseResult = await _repository.CreateAsync(product);

            return responseResult;
        }

        public async Task<ResponseResult> DeleteProduct(int id)
        {
            var responseResult = await _repository.DeleteAsync(id);

            return responseResult;
        }

        public async Task<ResponseResult> GetAllProducts()
        {
            var products = await _repository.GetAllAsync();
            List<ProductModel> productModels = new List<ProductModel>();

            if (products == null || !products.Any())
                return new ResponseResult(ResponseResultEnum.Error, "Products are empty");

            var categories = await GetCategoriesAsync();
            var currencies = await GetCurrenciesAsync();

            foreach (var item in products)
            {
                var category = categories.FirstOrDefault(c => c.CategoryID == item.CategoryID);
                var currency = currencies.FirstOrDefault(c => c.CurrencyID == item.CurrencyID);

                var model = _mapping.ToModel(item, category, currency);
                productModels.Add(model);
            }

            return new ResponseResult(ResponseResultEnum.Success, "", null, productModels);
        }

        public async Task<ResponseResult> GetProductById(int id)
        {
            var product = await _repository.GetByIdAsync(id);

            if (product == null) return new ResponseResult(ResponseResultEnum.Error, "Can not find product!");

            var categories = await GetCategoriesAsync();
            var currencies = await GetCurrenciesAsync();

            var category = categories.FirstOrDefault(c => c.CategoryID == product.CategoryID);
            var currency = currencies.FirstOrDefault(c => c.CurrencyID == product.CurrencyID);

            var productModel = _mapping.ToModel(product, category, currency);

            return new ResponseResult(ResponseResultEnum.Success, "", productModel);
        }

        public async Task<ResponseResult> GetProductsByCategoryId(int categoryId)
        {
            var products = await _repository.GetProductsByCategoryID(categoryId);
            List<ProductModel> productModels = new List<ProductModel>();

            if (products == null || !products.Any())
                return new ResponseResult(ResponseResultEnum.Error, "Products are empty");

            var categories = await GetCategoriesAsync();
            var currencies = await GetCurrenciesAsync();

            foreach (var item in products)
            {
                var category = categories.FirstOrDefault(c => c.CategoryID == item.CategoryID);
                var currency = currencies.FirstOrDefault(c => c.CurrencyID == item.CurrencyID);

                var model = _mapping.ToModel(item, category, currency);
                productModels.Add(model);
            }

            return new ResponseResult(ResponseResultEnum.Success, "", null, productModels);
        }

        public async Task<ResponseResult> GetProductsByCategoryIds(IEnumerable<int> categoryIds)
        {
            var products = await _repository.GetProductsByCategoryIDs(categoryIds);
            List<ProductModel> productModels = new List<ProductModel>();

            if (products == null || !products.Any())
                return new ResponseResult(ResponseResultEnum.Error, "Products are empty");

            var categories = await GetCategoriesAsync();
            var currencies = await GetCurrenciesAsync();

            foreach (var item in products)
            {
                var category = categories.FirstOrDefault(c => c.CategoryID == item.CategoryID);
                var currency = currencies.FirstOrDefault(c => c.CurrencyID == item.CurrencyID);

                var model = _mapping.ToModel(item, category, currency);

                productModels.Add(model);
            }

            return new ResponseResult(ResponseResultEnum.Success, "", null, productModels);
        }

        public async Task<ResponseResult> GetProductsByIds(IEnumerable<int> ids)
        {
            var products = await _repository.GetByIdsAsync(ids);
            List<ProductModel> productModels = new List<ProductModel>();

            if (products == null || !products.Any())
                return new ResponseResult(ResponseResultEnum.Error, "Products are empty");

            var categories = await GetCategoriesAsync();
            var currencies = await GetCurrenciesAsync();

            foreach (var item in products)
            {
                var category = categories.FirstOrDefault(c => c.CategoryID == item.CategoryID);
                var currency = currencies.FirstOrDefault(c => c.CurrencyID == item.CurrencyID);

                var model = _mapping.ToModel(item, category, currency);
                productModels.Add(model);
            }

            return new ResponseResult(ResponseResultEnum.Success, "", null, productModels);
        }

        public async Task<ResponseResult> UpdateProduct(ProductUpdateModel model)
        {
            if (model == null)
                return new ResponseResult(ResponseResultEnum.Error, "Can not find product!");

            var productToUpdate = await _repository.GetByIdAsync(model.ProductID);

            if (productToUpdate == null) return new ResponseResult(ResponseResultEnum.Error, "Can not find product!");

            productToUpdate = (Product)_mapping.ToEntity(productToUpdate, model);

            var responseResult = await _repository.UpdateAsync(productToUpdate);

            return responseResult;
        }
    }
}
