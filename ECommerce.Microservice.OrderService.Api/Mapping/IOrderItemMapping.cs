using ECommerce.Microservice.OrderService.Api.Entities;
using ECommerce.Microservice.OrderService.Api.Models;
using ECommerce.Microservice.OrderService.Api.Models.OrderItem;
using ECommerce.Microservice.SharedLibrary.BaseEntity;
using ECommerce.Microservice.SharedLibrary.BaseModel;
using ECommerce.Microservice.SharedLibrary.Mapping;

namespace ECommerce.Microservice.OrderService.Api.Mapping
{
    public interface IOrderItemMapping : IBaseMapping<BaseEntity, BaseModel>
    {
        BaseModel ToModel(OrderItem orderItem, ProductModel productModel);
    }

    public class OrderItemMapping : IOrderItemMapping
    {
        public BaseEntity ToEntity(BaseModel model)
        {
            BaseEntity? entity = null;

            if (model is OrderItemModel orderItemModel)
            {
                entity = new OrderItem()
                {
                    OrderItemID = orderItemModel.OrderItemID,
                    OrderID = orderItemModel.OrderID,
                    ProductID = orderItemModel.Product.ProductID,
                    Quantity = orderItemModel.Quantity
                };
            }
            else if (model is OrderItemCreateModel orderItemCreateModel)
            {
                entity = new OrderItem()
                {
                    OrderID = orderItemCreateModel.OrderID,
                    ProductID = orderItemCreateModel.ProductID,
                    Quantity = orderItemCreateModel.Quantity
                };
            }

            return entity;
        }

        public BaseEntity ToEntity(BaseEntity entity, BaseModel model)
        {
            if (entity is OrderItem orderItem && model is OrderItemUpdateModel orderItemUpdateModel)
            {
                orderItem.OrderItemID = orderItemUpdateModel.OrderItemID;
                orderItem.OrderID = orderItemUpdateModel.OrderID;
                orderItem.ProductID = orderItemUpdateModel.ProductID;
                orderItem.Quantity = orderItemUpdateModel.Quantity;
            }

            return entity;
        }

        public BaseModel ToModel(BaseEntity entity)
        {
            throw new NotImplementedException();
        }

        public BaseModel ToModel(OrderItem orderItem, ProductModel productModel)
        {
            OrderItemModel model = new OrderItemModel()
            {
                OrderItemID = orderItem.OrderItemID,
                OrderID = orderItem.OrderID,
                Product = productModel,
                Quantity = orderItem.Quantity
            };

            return model;
        }
    }
}
