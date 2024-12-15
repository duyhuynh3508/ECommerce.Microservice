using ECommerce.Microservice.ProductService.Api.DatabaseDbContext;
using ECommerce.Microservice.ProductService.Api.Entities;
using ECommerce.Microservice.SharedLibrary.IBaseRepository;
using ECommerce.Microservice.SharedLibrary.Logging;
using ECommerce.Microservice.SharedLibrary.Response;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Microservice.ProductService.Api.Repositories
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
    }

    public class CategoryRepository : ICategoryRepository
    {
        private readonly ProductDbContext _context;

        public CategoryRepository(ProductDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseResult> CreateAsync(Category entity)
        {
            if (entity == null)
                return new ResponseResult(ResponseResultEnum.Error, "Category cannot be empty");

            try
            {
                await _context.Categories.AddAsync(entity);
                await _context.SaveChangesAsync();

                return new ResponseResult(ResponseResultEnum.Success, "Category created successfully");
            }
            catch (Exception ex)
            {
                LoggingService.LogException(ex);
                return new ResponseResult(ResponseResultEnum.Error, $"Error occurs when creating category: {ex.Message}");
            }
        }

        public async Task<ResponseResult> DeleteAsync(int id)
        {
            if (id < 0)
                return new ResponseResult(ResponseResultEnum.Error, "CategoryId must be greater than 0");

            try
            {
                var category = await GetByIdAsync(id);
                if (category == null)
                    return new ResponseResult(ResponseResultEnum.Error, $"Category not found id: {id}");

                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();

                return new ResponseResult(ResponseResultEnum.Success, "Category deleted successfully");
            }
            catch (Exception ex)
            {
                LoggingService.LogException(ex);
                return new ResponseResult(ResponseResultEnum.Error, $"Error occurs when deleting category: {ex.Message}");
            }
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.CategoryID == id);
        }

        public async Task<IEnumerable<Category>> GetByIdsAsync(IEnumerable<int> ids)
        {
            if (ids == null || !ids.Any())
            {
                return Enumerable.Empty<Category>();
            }

            return await _context.Categories
                                 .Where(c => ids.Contains(c.CategoryID))
                                 .ToListAsync();
        }

        public async Task<ResponseResult> UpdateAsync(Category entity)
        {
            if (entity == null || entity.CategoryID < 0)
                return new ResponseResult(ResponseResultEnum.Error, "Category cannot be empty");

            try
            {
                _context.Categories.Update(entity);
                await _context.SaveChangesAsync();

                return new ResponseResult(ResponseResultEnum.Success, "Category updated successfully", entity);
            }
            catch (Exception ex)
            {
                LoggingService.LogException(ex);
                return new ResponseResult(ResponseResultEnum.Error, $"Error occurs when updating category: {ex.Message}");
            }
        }

        Task<IEnumerable<Category>> IBaseRepository<Category>.GetByNameAsync(string name)
        {
            throw new NotImplementedException();
        }
    }

}
