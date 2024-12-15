using ECommerce.Microservice.SharedLibrary.Response;

namespace ECommerce.Microservice.SharedLibrary.IBaseRepository
{
    public interface IBaseRepository<T> where T : class
    {
        Task<ResponseResult> CreateAsync(T entity);
        Task<ResponseResult> UpdateAsync(T entity);
        Task<ResponseResult> DeleteAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetByIdsAsync(IEnumerable<int> ids);
        Task<IEnumerable<T>> GetByNameAsync(string name);    
    }
}
