using ECommerce.Microservice.OrderService.Api.DatabaseDbContext;
using ECommerce.Microservice.OrderService.Api.Entities;
using ECommerce.Microservice.SharedLibrary.IBaseRepository;
using ECommerce.Microservice.SharedLibrary.Logging;
using ECommerce.Microservice.SharedLibrary.Response;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Microservice.OrderService.Api.Repositories
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        Task<IEnumerable<Order>> GetOrdersByUserID(int userID);
        Task<IEnumerable<Order>> GetOrdersByUserIDs(IEnumerable<int> userIDs);
    }

    public class OrderRepository : IOrderRepository
    {
        private readonly OrderDbContext _context;

        public OrderRepository(OrderDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseResult> CreateAsync(Order entity)
        {
            if (entity == null)
                return new ResponseResult(ResponseResultEnum.Error, "Order cannot be empty");

            try
            {
                await _context.AddAsync(entity);
                await _context.SaveChangesAsync();

                return new ResponseResult(ResponseResultEnum.Success, "Order created successfully");
            }
            catch (Exception ex)
            {
                LoggingService.LogException(ex);
                return new ResponseResult(ResponseResultEnum.Error, $"Error occurs when creating order: {ex.Message}");
            }
        }

        public async Task<ResponseResult> DeleteAsync(int id)
        {
            if (id < 0)
                return new ResponseResult(ResponseResultEnum.Error, "OrderId must be greater than 0");

            try
            {
                var order = await GetByIdAsync(id);

                if (order == null)
                    return new ResponseResult(ResponseResultEnum.Error, $"Cannot find order by id: {id}");

                _context.Remove(order);
                await _context.SaveChangesAsync();

                return new ResponseResult(ResponseResultEnum.Success, "Order deleted successfully");
            }
            catch (Exception ex)
            {
                LoggingService.LogException(ex);
                return new ResponseResult(ResponseResultEnum.Error, $"Error occurs when deleting order: {ex.Message}");
            }
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task<Order> GetByIdAsync(int id)
        {
            return await _context.Orders.FirstOrDefaultAsync(o => o.OrderID == id);
        }

        public async Task<IEnumerable<Order>> GetByIdsAsync(IEnumerable<int> ids)
        {
            if (ids == null || !ids.Any())
            {
                return Enumerable.Empty<Order>();
            }

            return await _context.Orders
                                 .Where(o => ids.Contains(o.OrderID))
                                 .ToListAsync();
        }

        public Task<IEnumerable<Order>> GetByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserID(int userID)
        {
            return await _context.Orders.Where(o => o.UserID == userID).ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserIDs(IEnumerable<int> userIDs)
        {
            return await _context.Orders.Where(o => userIDs.Contains(o.UserID)).ToListAsync();
        }

        public async Task<ResponseResult> UpdateAsync(Order entity)
        {
            if (entity == null || entity.OrderID < 0)
                return new ResponseResult(ResponseResultEnum.Error, "Order cannot be empty");

            try
            {
                _context.Orders.Update(entity);
                await _context.SaveChangesAsync();

                return new ResponseResult(ResponseResultEnum.Success, "Order updated successfully");
            }
            catch (Exception ex)
            {
                LoggingService.LogException(ex);
                return new ResponseResult(ResponseResultEnum.Error, $"Error occurs when updating order: {ex.Message}");
            }
        }
    }
}
