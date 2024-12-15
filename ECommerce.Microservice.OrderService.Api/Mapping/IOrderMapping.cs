using ECommerce.Microservice.OrderService.Api.Entities;
using ECommerce.Microservice.OrderService.Api.Enumerators;
using ECommerce.Microservice.OrderService.Api.Models.Order;
using ECommerce.Microservice.OrderService.Api.Models.OrderItem;
using ECommerce.Microservice.SharedLibrary.BaseEntity;
using ECommerce.Microservice.SharedLibrary.BaseModel;
using ECommerce.Microservice.SharedLibrary.Mapping;
using ECommerce.Microservice.UserService.Api.Models.User;

namespace ECommerce.Microservice.OrderService.Api.Mapping
{
    public interface IOrderMapping : IBaseMapping<BaseEntity, BaseModel>
    {
        BaseModel ToModel(Order order, UserModel userModel, List<OrderItemModel> orderItemModels);
    }

    public class OderMapping : IOrderMapping
    {
        public BaseEntity ToEntity(BaseModel model)
        {
            BaseEntity? entity = null;

            if (model is OrderCreateModel createModel)
            {
                entity = new Order()
                {
                    UserID = createModel.UserId,
                    OrderStatusID = (int)OrderStatusEnum.New,
                    TotalAmount = createModel.TotalAmount,
                };
            }

            return entity;
        }

        public BaseEntity ToEntity(BaseEntity entity, BaseModel model)
        {
            if (entity is Order order && model is OrderUpdateModel orderUpdateModel)
            {
                order.OrderID = orderUpdateModel.OrderID;
                order.UserID = orderUpdateModel.UserID;
                order.OrderStatusID = (int)orderUpdateModel.OrderStatusID;
                order.TotalAmount = orderUpdateModel.TotalAmount;
            }

            return entity;
        }

        public BaseModel ToModel(BaseEntity entity)
        {
            throw new NotImplementedException();
        }

        public BaseModel ToModel(Order order, UserModel userModel, List<OrderItemModel> orderItemModels)
        {
            OrderModel model = new OrderModel()
            {
                OrderID = order.OrderID,
                UserID = order.UserID,
                UserFirstName = userModel.FirstName,
                UserLastName = userModel.LastName,
                OrderStatusID = (OrderStatusEnum)order.OrderStatusID,
                TotalAmount = order.TotalAmount,
                OrderItems = orderItemModels
            };

            return model;
        }
    }
}
