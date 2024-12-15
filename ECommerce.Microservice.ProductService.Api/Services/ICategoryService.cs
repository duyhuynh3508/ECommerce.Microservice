using ECommerce.Microservice.ProductService.Api.Entities;
using ECommerce.Microservice.ProductService.Api.Mapping;
using ECommerce.Microservice.ProductService.Api.Models.Category;
using ECommerce.Microservice.ProductService.Api.RedisCaching;
using ECommerce.Microservice.ProductService.Api.Repositories;
using ECommerce.Microservice.SharedLibrary.Logging;
using ECommerce.Microservice.SharedLibrary.Response;

namespace ECommerce.Microservice.ProductService.Api.Services
{
    public interface ICategoryService
    {
        Task<ResponseResult> GetCategoryById(int id);
        Task<ResponseResult> GetAllCategories();
        Task<ResponseResult> CreateNewCategory(CategoryModel model);
        Task<ResponseResult> UpdateCategory(CategoryModel model);
        Task<ResponseResult> DeleteCategory(int id);
    }

    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly ICategoryMapping _mapping;
        private readonly IRedisCachingHandler _cacheHelper;
        private const string CategoryCacheKey = "categories";
        public CategoryService(ICategoryRepository repository, ICategoryMapping mapping, IRedisCachingHandler cacheHelper)
        {
            _repository = repository;
            _mapping = mapping;
            _cacheHelper = cacheHelper;
        }

        public async Task<ResponseResult> CreateNewCategory(CategoryModel model)
        {
            var category = (Category)_mapping.ToEntity(model);

            var responseResult = await _repository.CreateAsync(category);

            try
            {
                if (responseResult != null && responseResult.responseResult == ResponseResultEnum.Success)
                    await _cacheHelper.RemoveAsync(CategoryCacheKey);
            }
            catch (Exception ex)
            {
                LoggingService.LogException(ex);
                return responseResult;
            }

            return responseResult;
        }

        public async Task<ResponseResult> DeleteCategory(int id)
        {
            var responseResult = await _repository.DeleteAsync(id);

            return responseResult;
        }

        public async Task<ResponseResult> GetAllCategories()
        {
            var categories = await _repository.GetAllAsync();
            List<CategoryModel> categoryModels = new List<CategoryModel>();

            if (categories == null || !categories.Any())
                return new ResponseResult(ResponseResultEnum.Error, "Categories are empty");

            foreach (var item in categories)
            {
                var model = (CategoryModel)_mapping.ToModel(item);
                categoryModels.Add(model);
            }

            return new ResponseResult(ResponseResultEnum.Success, "", null, categoryModels);
        }

        public async Task<ResponseResult> GetCategoryById(int id)
        {
            var category = await _repository.GetByIdAsync(id);

            if (category == null) return new ResponseResult(ResponseResultEnum.Error, "Cannot find category!");

            var categoryModel = (CategoryModel)_mapping.ToModel(category);

            return new ResponseResult(ResponseResultEnum.Success, "", categoryModel);
        }

        public async Task<ResponseResult> UpdateCategory(CategoryModel model)
        {
            if (model == null)
                return new ResponseResult(ResponseResultEnum.Error, "Cannot find category!");

            var categoryToUpdate = await _repository.GetByIdAsync(model.CategoryID);

            if (categoryToUpdate == null) return new ResponseResult(ResponseResultEnum.Error, "Cannot find category!");

            categoryToUpdate = (Category)_mapping.ToEntity(categoryToUpdate, model);

            var responseResult = await _repository.UpdateAsync(categoryToUpdate);

            return responseResult;
        }
    }
}
