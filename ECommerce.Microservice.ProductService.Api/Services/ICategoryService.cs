using ECommerce.Microservice.ProductService.Api.Entities;
using ECommerce.Microservice.ProductService.Api.Mapping;
using ECommerce.Microservice.ProductService.Api.Models.Category;
using ECommerce.Microservice.ProductService.Api.Repositories;
using ECommerce.Microservice.SharedLibrary.BaseEntity;
using ECommerce.Microservice.SharedLibrary.BaseModel;
using ECommerce.Microservice.SharedLibrary.Mapping;
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
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICategoryMapping _categoryMapping;

        public CategoryService(ICategoryRepository categoryRepository, ICategoryMapping categoryMapping)
        {
            _categoryRepository = categoryRepository;
            _categoryMapping = categoryMapping;
        }

        public async Task<ResponseResult> CreateNewCategory(CategoryModel model)
        {
            var category = (Category)_categoryMapping.ToEntity(model);

            var responseResult = await _categoryRepository.CreateAsync(category);

            return responseResult;
        }

        public async Task<ResponseResult> DeleteCategory(int id)
        {
            var responseResult = await _categoryRepository.DeleteAsync(id);

            return responseResult;
        }

        public async Task<ResponseResult> GetAllCategories()
        {
            var categories = await _categoryRepository.GetAllAsync();
            List<CategoryModel> categoryModels = new List<CategoryModel>();

            if (categories == null || !categories.Any())
                return new ResponseResult(ResponseResultEnum.Error, "Categories are empty");

            foreach (var item in categories)
            {
                var model = (CategoryModel)_categoryMapping.ToModel(item);
                categoryModels.Add(model);
            }

            return new ResponseResult(ResponseResultEnum.Success, "", null, categoryModels);
        }

        public async Task<ResponseResult> GetCategoryById(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);

            if (category == null) return new ResponseResult(ResponseResultEnum.Error, "Cannot find category!");

            var categoryModel = (CategoryModel)_categoryMapping.ToModel(category);

            return new ResponseResult(ResponseResultEnum.Success, "", categoryModel);
        }

        public async Task<ResponseResult> UpdateCategory(CategoryModel model)
        {
            if (model == null)
                return new ResponseResult(ResponseResultEnum.Error, "Cannot find category!");

            var categoryToUpdate = await _categoryRepository.GetByIdAsync(model.CategoryID);

            if (categoryToUpdate == null) return new ResponseResult(ResponseResultEnum.Error, "Cannot find category!");

            categoryToUpdate = (Category)_categoryMapping.ToEntity(categoryToUpdate, model);

            var responseResult = await _categoryRepository.UpdateAsync(categoryToUpdate);

            return responseResult;
        }
    }
}
