using ECommerce.Microservice.ProductService.Api.Entities;
using ECommerce.Microservice.ProductService.Api.Models.Category;
using ECommerce.Microservice.SharedLibrary.BaseEntity;
using ECommerce.Microservice.SharedLibrary.BaseModel;
using ECommerce.Microservice.SharedLibrary.Mapping;

namespace ECommerce.Microservice.ProductService.Api.Mapping
{
    public interface ICategoryMapping : IBaseMapping<BaseEntity, BaseModel>
    {
    }
    public class CategoryMapping : ICategoryMapping
    {
        public BaseEntity ToEntity(BaseModel model)
        {
            BaseEntity? entity = null;

            if (model is CategoryModel categoryModel)
            {
                entity = new Category()
                {
                    CategoryID = categoryModel.CategoryID,
                    CategoryName = categoryModel.CategoryName
                };
            }

            return entity;
        }

        public BaseEntity ToEntity(BaseEntity entity, BaseModel model)
        {
            if (entity is Category category)
            {
                if (model is CategoryModel categoryUpdateModel)
                {
                    category.CategoryID = categoryUpdateModel.CategoryID;
                    category.CategoryName = categoryUpdateModel.CategoryName;
                }
            }

            return entity;
        }

        public BaseModel ToModel(BaseEntity entity)
        {
            BaseModel model = null;

            if (entity is Category category)
            {
                model = new CategoryModel()
                {
                    CategoryID = category.CategoryID,
                    CategoryName = category.CategoryName
                };
            }

            return model;
        }
    }
}
