using ECommerce.Microservice.OrderService.Api.Entities;
using ECommerce.Microservice.OrderService.Api.Mapping;
using ECommerce.Microservice.OrderService.Api.Models.OrderItem;
using ECommerce.Microservice.OrderService.Api.Repositories;
using ECommerce.Microservice.SharedLibrary.Response;

namespace ECommerce.Microservice.OrderService.Api.Services
{
    public interface IOrderItemService
    {
        Task<ResponseResult> GetOrderItemsByOrderID(int orderId);
        Task<ResponseResult> GetOrderItemByID(int id);
        Task<ResponseResult> GetOrderItemsByIDs(IEnumerable<int> ids);
        Task<ResponseResult> GetOrderItemsByOrderIDs(IEnumerable<int> orderIds);
        Task<ResponseResult> CreateNewOrderItem(OrderItemCreateModel model);
        Task<ResponseResult> UpdateOrderItem(OrderItemUpdateModel model);
        Task<ResponseResult> DeleteOrderItem(int id);
    }

    public class OrderItemService : IOrderItemService
    {
        private readonly IOrderItemRepository _repository;
        private readonly IOrderItemMapping _mapping;

        public OrderItemService(IOrderItemRepository repository, IOrderItemMapping mapping)
        {
            _repository = repository;
            _mapping = mapping;
        }

        public async Task<ResponseResult> CreateNewOrderItem(OrderItemCreateModel model)
        {
            var orderItem = (OrderItem)_mapping.ToEntity(model);
            var responseResult = await _repository.CreateAsync(orderItem);
            return responseResult;
        }

        public async Task<ResponseResult> DeleteOrderItem(int id)
        {
            var responseResult = await _repository.DeleteAsync(id);
            return responseResult;
        }

        public async Task<ResponseResult> GetOrderItemsByOrderID(int orderId)
        {
            var orderItems = await _repository.GetOrderItemsByOrderID(orderId);

            if (orderItems == null || !orderItems.Any())
                return new ResponseResult(ResponseResultEnum.Error, "Order items are empty");

            return new ResponseResult(ResponseResultEnum.Success, "", null, orderItems);
        }

        public async Task<ResponseResult> GetOrderItemByID(int id)
        {
            var orderItem = await _repository.GetByIdAsync(id);

            if (orderItem == null)
                return new ResponseResult(ResponseResultEnum.Error, "Can not find order item!");

            return new ResponseResult(ResponseResultEnum.Success, "", orderItem);
        }

        public async Task<ResponseResult> GetOrderItemsByIDs(IEnumerable<int> ids)
        {
            var orderItems = await _repository.GetByIdsAsync(ids);

            if (orderItems == null || !orderItems.Any())
                return new ResponseResult(ResponseResultEnum.Error, "Order items are empty");

            return new ResponseResult(ResponseResultEnum.Success, "", null, orderItems);
        }

        public async Task<ResponseResult> GetOrderItemsByOrderIDs(IEnumerable<int> orderIds)
        {
            var orderItems = await _repository.GetOrderItemsByOrderIDs(orderIds);

            if (orderItems == null || !orderItems.Any())
                return new ResponseResult(ResponseResultEnum.Error, "Order items are empty");

            return new ResponseResult(ResponseResultEnum.Success, "", null, orderItems);
        }

        public async Task<ResponseResult> UpdateOrderItem(OrderItemUpdateModel model)
        {
            var orderItemToUpdate = await _repository.GetByIdAsync(model.OrderItemID);

            if (orderItemToUpdate == null)
                return new ResponseResult(ResponseResultEnum.Error, "Can not find order item!");

            orderItemToUpdate = (OrderItem)_mapping.ToEntity(orderItemToUpdate, model);

            var responseResult = await _repository.UpdateAsync(orderItemToUpdate);
            return responseResult;
        }
    }
}
