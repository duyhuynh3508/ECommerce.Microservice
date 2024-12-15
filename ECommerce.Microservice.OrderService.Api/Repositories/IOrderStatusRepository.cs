using ECommerce.Microservice.OrderService.Api.DatabaseDbContext;
using ECommerce.Microservice.OrderService.Api.Entities;
using ECommerce.Microservice.SharedLibrary.IBaseRepository;
using ECommerce.Microservice.SharedLibrary.Logging;
using ECommerce.Microservice.SharedLibrary.Response;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Microservice.OrderService.Api.Repositories
{
    public interface IOrderStatusRepository : IBaseRepository<OrderStatus>
    {
        Task<OrderStatus> GetOrderStatusByNameAsync(string name);
    }

    public class OrderStatusRepository : IOrderStatusRepository
    {
        private readonly OrderDbContext _context;

        public OrderStatusRepository(OrderDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseResult> CreateAsync(OrderStatus entity)
        {
            if (entity == null || string.IsNullOrWhiteSpace(entity.OrderStatusName))
                return new ResponseResult(ResponseResultEnum.Error, "OrderStatus cannot be empty");

            try
            {
                await _context.AddAsync(entity);
                await _context.SaveChangesAsync();

                return new ResponseResult(ResponseResultEnum.Success, "OrderStatus created successfully");
            }
            catch (Exception ex)
            {
                LoggingService.LogException(ex);
                return new ResponseResult(ResponseResultEnum.Error, $"Error occurs when creating order status: {ex.Message}");
            }
        }

        public async Task<ResponseResult> DeleteAsync(int id)
        {
            if (id < 0)
                return new ResponseResult(ResponseResultEnum.Error, "OrderStatusID must be greater than 0");

            try
            {
                var orderStatus = await GetByIdAsync(id);

                if (orderStatus == null)
                    return new ResponseResult(ResponseResultEnum.Error, $"Cannot find order status by id: {id}");

                _context.Remove(orderStatus);
                await _context.SaveChangesAsync();

                return new ResponseResult(ResponseResultEnum.Success, "OrderStatus deleted successfully");
            }
            catch (Exception ex)
            {
                LoggingService.LogException(ex);
                return new ResponseResult(ResponseResultEnum.Error, $"Error occurs when deleting order status: {ex.Message}");
            }
        }

        public async Task<IEnumerable<OrderStatus>> GetAllAsync()
        {
            return await _context.OrderStatuses.ToListAsync();
        }

        public async Task<OrderStatus> GetByIdAsync(int id)
        {
            return await _context.OrderStatuses.FirstOrDefaultAsync(os => os.OrderStatusID == id);
        }

        public async Task<IEnumerable<OrderStatus>> GetByIdsAsync(IEnumerable<int> ids)
        {
            if (ids == null || !ids.Any())
            {
                return Enumerable.Empty<OrderStatus>();
            }

            return await _context.OrderStatuses
                                 .Where(os => ids.Contains(os.OrderStatusID))
                                 .ToListAsync();
        }

        public Task<IEnumerable<OrderStatus>> GetByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<OrderStatus> GetOrderStatusByNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return null;

            return await _context.OrderStatuses.FirstOrDefaultAsync(os => os.OrderStatusName == name);
        }

        public async Task<ResponseResult> UpdateAsync(OrderStatus entity)
        {
            if (entity == null || entity.OrderStatusID < 0 || string.IsNullOrWhiteSpace(entity.OrderStatusName))
                return new ResponseResult(ResponseResultEnum.Error, "OrderStatus cannot be empty");

            try
            {
                _context.OrderStatuses.Update(entity);
                await _context.SaveChangesAsync();

                return new ResponseResult(ResponseResultEnum.Success, "OrderStatus updated successfully");
            }
            catch (Exception ex)
            {
                LoggingService.LogException(ex);
                return new ResponseResult(ResponseResultEnum.Error, $"Error occurs when updating order status: {ex.Message}");
            }
        }
    }
}
