using ECommerce.Microservice.ProductService.Api.Entities;
using ECommerce.Microservice.ProductService.Api.Enumerators;
using ECommerce.Microservice.SharedLibrary.BaseEntity;
using ECommerce.Microservice.SharedLibrary.BaseModel;
using ECommerce.Microservice.ProductService.Api.Models.Product;
using ECommerce.Microservice.SharedLibrary.Mapping;

namespace ECommerce.Microservice.ProductService.Api.Mapping
{
    public interface IProductMapping : IBaseMapping<BaseEntity, BaseModel>
    {
    }

    public class ProductMapping : IProductMapping
    {
        public BaseEntity ToEntity(BaseModel model)
        {
            BaseEntity? enity = null;

            if(model is ProductModel productModel)
            {
                enity = new Product()
                {
                    ProductID = productModel.ProductID,
                    CategoryID = (int)productModel.CategoryID,
                    CurrencyID = (int)productModel.CurrencyID,
                    ProductName = productModel.ProductName,
                    ProductPrice = productModel.ProductPrice,
                    Description = productModel.Description,
                    ImageURL = productModel.ImageURL,
                    CreatedDate = productModel.CreatedDate,
                    LastUpdatedDate = productModel.LastUpdatedDate,
                };
            }
            else if (model is ProductCreateModel productCreateModel)
            {
                enity = new Product()
                {
                    CategoryID = (int)productCreateModel.CategoryID,
                    CurrencyID = (int)productCreateModel.CurrencyID,
                    ProductName = productCreateModel.ProductName,
                    ProductPrice = productCreateModel.ProductPrice,
                    Description = productCreateModel.Description,
                    ImageURL = productCreateModel.ImageURL,
                    CreatedDate = DateTime.Now
                };
            }

            return enity;
        }

        public BaseEntity ToEntity(BaseEntity entity, BaseModel model)
        {
            if (entity is Product product)
            {
                if(model is ProductUpdateModel productUpdateModel)
                {
                    product.ProductID = productUpdateModel.ProductID;
                    product.CategoryID = (int)productUpdateModel.CategoryID;
                    product.CurrencyID = (int)productUpdateModel.CurrencyID;
                    product.ProductPrice = productUpdateModel.ProductPrice;
                    product.Description = productUpdateModel.Description;
                    product.ImageURL = productUpdateModel.ImageURL;
                    product.LastUpdatedDate = DateTime.Now;
                }
            }

            return entity;
        }

        public BaseModel ToModel(BaseEntity entity)
        {
            BaseModel model = null;

            if (entity is Product product)
            {
                model = new ProductModel()
                {
                    ProductID = product.ProductID,
                    CategoryID = (CategoryEnum)product.CategoryID,
                    CurrencyID = (CurrencyEnum)product.CurrencyID,
                    ProductName = product.ProductName,
                    ProductPrice = product.ProductPrice,
                    Description = product.Description,
                    ImageURL = product.ImageURL,
                    CreatedDate = DateTime.Now,
                    LastUpdatedDate = DateTime.Now
                };
            }

            return model;
        }
    }
}
