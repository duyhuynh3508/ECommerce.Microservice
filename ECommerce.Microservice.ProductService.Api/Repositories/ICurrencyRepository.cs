using ECommerce.Microservice.ProductService.Api.DatabaseDbContext;
using ECommerce.Microservice.ProductService.Api.Entities;
using ECommerce.Microservice.SharedLibrary.IBaseRepository;
using ECommerce.Microservice.SharedLibrary.Logging;
using ECommerce.Microservice.SharedLibrary.Response;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Microservice.ProductService.Api.Repositories
{
    public interface ICurrencyRepository : IBaseRepository<Currency>
    {
    }

    public class CurrencyRepository : ICurrencyRepository
    {
        private readonly ProductDbContext _context;

        public CurrencyRepository(ProductDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseResult> CreateAsync(Currency entity)
        {
            if (entity == null)
                return new ResponseResult(ResponseResultEnum.Error, "Currency cannot be empty");

            try
            {
                await _context.Currencies.AddAsync(entity);
                await _context.SaveChangesAsync();

                return new ResponseResult(ResponseResultEnum.Success, "Currency created successfully", entity);
            }
            catch (Exception ex)
            {
                LoggingService.LogException(ex);
                return new ResponseResult(ResponseResultEnum.Error, $"Error occurs when creating currency: {ex.Message}");
            }
        }

        public async Task<ResponseResult> DeleteAsync(int id)
        {
            if (id < 0)
                return new ResponseResult(ResponseResultEnum.Error, "CurrencyId must be greater than 0");

            try
            {
                var currency = await _context.Currencies.FindAsync(id);
                if (currency == null)
                    return new ResponseResult(ResponseResultEnum.Error, "Currency not found");

                _context.Currencies.Remove(currency);
                await _context.SaveChangesAsync();

                return new ResponseResult(ResponseResultEnum.Success, "Currency deleted successfully");
            }
            catch (Exception ex)
            {
                LoggingService.LogException(ex);
                return new ResponseResult(ResponseResultEnum.Error, $"Error occurs when deleting currency: {ex.Message}");
            }
        }

        public async Task<IEnumerable<Currency>> GetAllAsync()
        {
            return await _context.Currencies.ToListAsync();
        }

        public async Task<Currency> GetByIdAsync(int id)
        {
            return await _context.Currencies.FirstOrDefaultAsync(c => c.CurrencyID == id);
        }

        public async Task<IEnumerable<Currency>> GetByIdsAsync(IEnumerable<int> ids)
        {
            if (ids == null || !ids.Any())
            {
                return Enumerable.Empty<Currency>();
            }

            return await _context.Currencies
                                 .Where(c => ids.Contains(c.CurrencyID))
                                 .ToListAsync();
        }

        public Task<Currency> GetByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseResult> UpdateAsync(Currency entity)
        {
            if (entity == null || entity.CurrencyID < 0)
                return new ResponseResult(ResponseResultEnum.Error, "Currency cannot be empty");

            try
            {
                _context.Currencies.Update(entity);
                await _context.SaveChangesAsync();

                return new ResponseResult(ResponseResultEnum.Success, "Currency updated successfully", entity);
            }
            catch (Exception ex)
            {
                LoggingService.LogException(ex);
                return new ResponseResult(ResponseResultEnum.Error, $"Error occurs when updating currency: {ex.Message}");
            }
        }
    }

}
