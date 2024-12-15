using ECommerce.Microservice.OrderService.Api.Entities;
using ECommerce.Microservice.OrderService.Api.Models;
using ECommerce.Microservice.SharedLibrary.BaseEntity;
using ECommerce.Microservice.SharedLibrary.BaseModel;
using ECommerce.Microservice.SharedLibrary.Mapping;

namespace ECommerce.Microservice.OrderService.Api.Mapping
{
    public interface IOrderStatusMapping : IBaseMapping<BaseEntity, BaseModel>
    {
    }

    public class OrderStatusMapping : IOrderStatusMapping
    {
        public BaseEntity ToEntity(BaseModel model)
        {
            BaseEntity? entity = null;
            if (model is OrderStatusModel orderStatusModel)
            {
                entity = new OrderStatus()
                {
                    OrderStatusID = orderStatusModel.OrderStatusID,
                    OrderStatusName = orderStatusModel.OrderStatusName,
                };
            }

            return entity;
        }

        public BaseEntity ToEntity(BaseEntity entity, BaseModel model)
        {
            if (entity is OrderStatus orderStatus && model is OrderStatusModel orderStatusModel)
            {
                orderStatus.OrderStatusID = orderStatusModel.OrderStatusID;
                orderStatus.OrderStatusName = orderStatusModel.OrderStatusName;
            }

            return entity;
        }

        public BaseModel ToModel(BaseEntity entity)
        {
            BaseModel? model = null;
            if (entity is OrderStatus orderStatus)
            {
                model = new OrderStatusModel()
                {
                    OrderStatusID = orderStatus.OrderStatusID,
                    OrderStatusName = orderStatus.OrderStatusName
                };
            }

            return model;
        }
    }
}
