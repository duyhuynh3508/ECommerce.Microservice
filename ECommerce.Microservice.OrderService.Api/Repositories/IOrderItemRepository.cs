using ECommerce.Microservice.OrderService.Api.DatabaseDbContext;
using ECommerce.Microservice.OrderService.Api.Entities;
using ECommerce.Microservice.SharedLibrary.IBaseRepository;
using ECommerce.Microservice.SharedLibrary.Logging;
using ECommerce.Microservice.SharedLibrary.Response;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Microservice.OrderService.Api.Repositories
{
    public interface IOrderItemRepository : IBaseRepository<OrderItem>
    {
        Task<IEnumerable<OrderItem>> GetOrderItemsByOrderID(int orderID);
        Task<IEnumerable<OrderItem>> GetOrderItemsByOrderIDs(IEnumerable<int> orderIDs);
    }

    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly OrderDbContext _context;

        public OrderItemRepository(OrderDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseResult> CreateAsync(OrderItem entity)
        {
            if (entity == null)
                return new ResponseResult(ResponseResultEnum.Error, "OrderItem cannot be empty");

            try
            {
                await _context.AddAsync(entity);
                await _context.SaveChangesAsync();

                return new ResponseResult(ResponseResultEnum.Success, "OrderItem created successfully");
            }
            catch (Exception ex)
            {
                LoggingService.LogException(ex);
                return new ResponseResult(ResponseResultEnum.Error, $"Error occurs when creating order item: {ex.Message}");
            }
        }

        public async Task<ResponseResult> DeleteAsync(int id)
        {
            if (id < 0)
                return new ResponseResult(ResponseResultEnum.Error, "OrderItemID must be greater than 0");

            try
            {
                var orderItem = await GetByIdAsync(id);

                if (orderItem == null)
                    return new ResponseResult(ResponseResultEnum.Error, $"Cannot find order item by id: {id}");

                _context.Remove(orderItem);
                await _context.SaveChangesAsync();

                return new ResponseResult(ResponseResultEnum.Success, "OrderItem deleted successfully");
            }
            catch (Exception ex)
            {
                LoggingService.LogException(ex);
                return new ResponseResult(ResponseResultEnum.Error, $"Error occurs when deleting order item: {ex.Message}");
            }
        }

        public async Task<IEnumerable<OrderItem>> GetAllAsync()
        {
            return await _context.OrderItems.ToListAsync();
        }

        public async Task<OrderItem> GetByIdAsync(int id)
        {
            return await _context.OrderItems.FirstOrDefaultAsync(oi => oi.OrderItemID == id);
        }

        public async Task<IEnumerable<OrderItem>> GetByIdsAsync(IEnumerable<int> ids)
        {
            if (ids == null || !ids.Any())
            {
                return Enumerable.Empty<OrderItem>();
            }

            return await _context.OrderItems
                                 .Where(oi => ids.Contains(oi.OrderItemID))
                                 .ToListAsync();
        }

        public Task<IEnumerable<OrderItem>> GetByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<OrderItem>> GetOrderItemsByOrderID(int orderID)
        {
            return await _context.OrderItems.Where(oi => oi.OrderID == orderID).ToListAsync();
        }

        public async Task<IEnumerable<OrderItem>> GetOrderItemsByOrderIDs(IEnumerable<int> orderIDs)
        {
            return await _context.OrderItems.Where(oi => orderIDs.Contains(oi.OrderID)).ToListAsync();
        }

        public async Task<ResponseResult> UpdateAsync(OrderItem entity)
        {
            if (entity == null || entity.OrderItemID < 0)
                return new ResponseResult(ResponseResultEnum.Error, "OrderItem cannot be empty");

            try
            {
                _context.OrderItems.Update(entity);
                await _context.SaveChangesAsync();

                return new ResponseResult(ResponseResultEnum.Success, "OrderItem updated successfully");
            }
            catch (Exception ex)
            {
                LoggingService.LogException(ex);
                return new ResponseResult(ResponseResultEnum.Error, $"Error occurs when updating order item: {ex.Message}");
            }
        }
    }
}
