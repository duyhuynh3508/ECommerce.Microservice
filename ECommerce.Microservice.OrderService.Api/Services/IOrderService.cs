using ECommerce.Microservice.OrderService.Api.Entities;
using ECommerce.Microservice.OrderService.Api.Mapping;
using ECommerce.Microservice.OrderService.Api.Models.Order;
using ECommerce.Microservice.OrderService.Api.Repositories;
using ECommerce.Microservice.SharedLibrary.Response;

namespace ECommerce.Microservice.OrderService.Api.Services
{
    public interface IOrderService
    {
        Task<ResponseResult> GetOrderByID(int id);
        Task<ResponseResult> GetOrdersByIDs(IEnumerable<int> ids);
        Task<ResponseResult> GetOrdersByUserID(int userId);
        Task<ResponseResult> GetOrdersByUserIDs(IEnumerable<int> userIds);
        Task<ResponseResult> GetAllOrders();
        Task<ResponseResult> CreateNewOrder(OrderCreateModel model);
        Task<ResponseResult> UpdateOrder(OrderUpdateModel model);
        Task<ResponseResult> DeleteOrder(int id);
    }

    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repository;
        private readonly IOrderMapping _mapping;

        public OrderService(IOrderRepository repository, IOrderMapping mapping)
        {
            _repository = repository;
            _mapping = mapping;
        }

        public async Task<ResponseResult> CreateNewOrder(OrderCreateModel model)
        {
            var order = (Order)_mapping.ToEntity(model);
            var responseResult = await _repository.CreateAsync(order);
            return responseResult;
        }

        public async Task<ResponseResult> DeleteOrder(int id)
        {
            var responseResult = await _repository.DeleteAsync(id);
            return responseResult;
        }

        public async Task<ResponseResult> GetAllOrders()
        {
            var orders = await _repository.GetAllAsync();

            if (orders == null || !orders.Any())
                return new ResponseResult(ResponseResultEnum.Error, "Orders are empty");

            return new ResponseResult(ResponseResultEnum.Success, "", null, orders);
        }

        public async Task<ResponseResult> GetOrderByID(int id)
        {
            var order = await _repository.GetByIdAsync(id);

            if (order == null) return new ResponseResult(ResponseResultEnum.Error, "Can not find order!");

            return new ResponseResult(ResponseResultEnum.Success, "", order);
        }

        public async Task<ResponseResult> GetOrdersByIDs(IEnumerable<int> ids)
        {
            var orders = await _repository.GetByIdsAsync(ids);

            if (orders == null || !orders.Any())
                return new ResponseResult(ResponseResultEnum.Error, "Orders are empty");

            return new ResponseResult(ResponseResultEnum.Success, "", null, orders);
        }

        public async Task<ResponseResult> GetOrdersByUserID(int userId)
        {
            var orders = await _repository.GetOrdersByUserID(userId);

            if (orders == null || !orders.Any())
                return new ResponseResult(ResponseResultEnum.Error, "Orders are empty");

            return new ResponseResult(ResponseResultEnum.Success, "", null, orders);
        }

        public async Task<ResponseResult> GetOrdersByUserIDs(IEnumerable<int> userIds)
        {
            var orders = await _repository.GetOrdersByUserIDs(userIds);

            if (orders == null || !orders.Any())
                return new ResponseResult(ResponseResultEnum.Error, "Orders are empty");

            return new ResponseResult(ResponseResultEnum.Success, "", null, orders);
        }

        public async Task<ResponseResult> UpdateOrder(OrderUpdateModel model)
        {
            var orderToUpdate = await _repository.GetByIdAsync(model.OrderID);

            if (orderToUpdate == null)
                return new ResponseResult(ResponseResultEnum.Error, "Can not find order!");

            orderToUpdate = (Order)_mapping.ToEntity(orderToUpdate, model);

            var responseResult = await _repository.UpdateAsync(orderToUpdate);
            return responseResult;
        }
    }
}
