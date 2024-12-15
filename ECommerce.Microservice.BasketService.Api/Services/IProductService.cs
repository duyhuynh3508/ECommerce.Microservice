using ECommerce.Microservice.ProductService.Api.Entities;
using ECommerce.Microservice.ProductService.Api.Mapping;
using ECommerce.Microservice.ProductService.Api.Models.Product;
using ECommerce.Microservice.ProductService.Api.Repositories;
using ECommerce.Microservice.SharedLibrary.Response;

namespace ECommerce.Microservice.ProductService.Api.Services
{
    public interface IProductService
    {
        Task<ResponseResult> GetProductById(int id);
        Task<ResponseResult> GetProductsByIds(IEnumerable<int> ids);
        Task<ResponseResult> GetAllProducts();
        Task<ResponseResult> CreateNewProduct(ProductCreateModel model);
        Task<ResponseResult> UpdateProduct(ProductUpdateModel model);
        Task<ResponseResult> DeleteProduct(int id);
    }
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IProductMapping _mapping;

        public ProductService(IProductRepository productRepository, IProductMapping productMapping)
        {
            _repository = productRepository;
            _mapping = productMapping;
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

            foreach (var item in products)
            {
                var model = (ProductModel)_mapping.ToModel(item);
                productModels.Add(model);
            }

            return new ResponseResult(ResponseResultEnum.Success, "", null, productModels);
        }

        public async Task<ResponseResult> GetProductById(int id)
        {
            var product = await _repository.GetByIdAsync(id);

            if (product == null) return new ResponseResult(ResponseResultEnum.Error, "Can not find product!");

            var productModel = (ProductModel)_mapping.ToModel(product);

            return new ResponseResult(ResponseResultEnum.Success, "", productModel);
        }

        public async Task<ResponseResult> GetProductsByIds(IEnumerable<int> ids)
        {
            var products = await _repository.GetByIdsAsync(ids);
            List<ProductModel> productModels = new List<ProductModel>();

            if (products == null || !products.Any())
                return new ResponseResult(ResponseResultEnum.Error, "Products are empty");

            foreach (var item in products)
            {
                var model = (ProductModel)_mapping.ToModel(item);
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
