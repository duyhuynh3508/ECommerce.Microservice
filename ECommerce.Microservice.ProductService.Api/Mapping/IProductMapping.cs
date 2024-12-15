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
        ProductModel ToModel(Product product, Category category, Currency currency);
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
                    ImageURL = productModel.ImageURL
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
                    ImageURL = productCreateModel.ImageURL
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
                    ImageURL = product.ImageURL
                };
            }

            return model;
        }

        public ProductModel ToModel(Product product, Category category, Currency currency)
        {
            ProductModel model = new ProductModel();

            if (product != null)
            {
                model.ProductID = product.ProductID;
                model.ProductName = product.ProductName;
                model.ProductPrice = product.ProductPrice;
                model.Description = product.Description;
                model.ImageURL = product.ImageURL;
            }

            if (category != null)
            {
                model.CategoryID = (CategoryEnum)category.CategoryID;
                model.CategoryName = category.CategoryName;
            }

            if (currency != null)
            {
                model.CurrencyID = (CurrencyEnum)currency.CurrencyID;
                model.CurrencyName = currency.CurrencyName;
            }

            return model;
        }
    }
}
