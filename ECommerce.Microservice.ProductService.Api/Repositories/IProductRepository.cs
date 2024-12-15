﻿using ECommerce.Microservice.ProductService.Api.DatabaseDbContext;
using ECommerce.Microservice.ProductService.Api.Entities;
using ECommerce.Microservice.ProductService.Api.Mapping;
using ECommerce.Microservice.ProductService.Api.Models.Product;
using ECommerce.Microservice.SharedLibrary.IBaseRepository;
using ECommerce.Microservice.SharedLibrary.Logging;
using ECommerce.Microservice.SharedLibrary.Response;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Microservice.ProductService.Api.Repositories
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<IEnumerable<Product>> GetProductsByCategoryID(int categoryID);
        Task<IEnumerable<Product>> GetProductsByCategoryIDs(IEnumerable<int> categoryIDs);
    }

    public class ProductRepository : IProductRepository
    {
        private readonly ProductDbContext _context;
        public ProductRepository(ProductDbContext context)
        {
            _context = context;
        }
        public async Task<ResponseResult> CreateAsync(Product entity)
        {
            if (entity == null)
                return new ResponseResult(ResponseResultEnum.Error, "Product can not be empty");

            try
            {
                await _context.AddAsync(entity);
                await _context.SaveChangesAsync();

                return new ResponseResult(ResponseResultEnum.Success, "Product created successfully");
            }
            catch (Exception ex)
            {
                LoggingService.LogException(ex);
                return new ResponseResult(ResponseResultEnum.Error, $"Error occurs when creating product: {ex.Message}");
            }
        }

        public async Task<ResponseResult> DeleteAsync(int id)
        {
            if (id < 0)
                return new ResponseResult(ResponseResultEnum.Error, "ProductId must be greater than 0");
            try
            {
                var product = await GetByIdAsync(id);

                if (product == null)
                    return new ResponseResult(ResponseResultEnum.Error, $"Cannot found product by id: {id}");

                _context.Remove(product);
                await _context.SaveChangesAsync();

                return new ResponseResult(ResponseResultEnum.Success, "Product deleted successfully");
            }
            catch (Exception ex)
            {
                LoggingService.LogException(ex);
                return new ResponseResult(ResponseResultEnum.Error, $"Error occurs when deleting product: {ex.Message}");
            }
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.ProductID == id);
        }

        public async Task<IEnumerable<Product>> GetByIdsAsync(IEnumerable<int> ids)
        {
            if (ids == null || !ids.Any())
            {
                return Enumerable.Empty<Product>();
            }

            return await _context.Products
                                 .Where(p => ids.Contains(p.ProductID))
                                 .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetByNameAsync(string name)
        {
            return await _context.Products.Where(p => p.ProductName.Contains(name)).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryID(int categoryID)
        {
            return await _context.Products.Where(p => p.CategoryID.Equals(categoryID)).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryIDs(IEnumerable<int> categoryIDs)
        {
            return await _context.Products.Where(p => categoryIDs.Contains(p.CategoryID)).ToListAsync();
        }

        public async Task<ResponseResult> UpdateAsync(Product entity)
        {
            if (entity == null || entity.ProductID < 0)
                return new ResponseResult(ResponseResultEnum.Error, "Product can not be empty");

            try
            {
                _context.Products.Update(entity);
                await _context.SaveChangesAsync();

                return new ResponseResult(ResponseResultEnum.Success, "Product updated successfully");
            }
            catch (Exception ex)
            {
                LoggingService.LogException(ex);
                return new ResponseResult(ResponseResultEnum.Error, $"Error occurs when updating product: {ex.Message}");
            }
        }
    }
}
