using ECommerce.Microservice.SharedLibrary.IBaseRepository;
using ECommerce.Microservice.SharedLibrary.Logging;
using ECommerce.Microservice.SharedLibrary.Response;
using ECommerce.Microservice.UserService.Api.DatabaseDbContext;
using ECommerce.Microservice.UserService.Api.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Microservice.UserService.Api.Repositories
{
    public interface IUserRoleRepository : IBaseRepository<UserRole>
    {
        Task<ResponseResult> GetRolesByUserId(int id);
    }

    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly UserDbContext _context;

        public UserRoleRepository(UserDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseResult> CreateAsync(UserRole entity)
        {
            if (entity == null)
                return new ResponseResult(ResponseResultEnum.Error, "Role can not be empty");
            try
            {
                await _context.UserRoles.AddAsync(entity);
                await _context.SaveChangesAsync();

                return new ResponseResult(ResponseResultEnum.Success, "Role created successfully");
            }
            catch (Exception ex)
            {
                LoggingService.LogException(ex);
                return new ResponseResult(ResponseResultEnum.Error, $"Error occurs when creating role: {ex.Message}");
            }
        }

        public async Task<ResponseResult> DeleteAsync(int id)
        {
            if (id < 0)
                return new ResponseResult(ResponseResultEnum.Error, "RoleId must be greater than 0");

            try
            {
                var role = await GetByIdAsync(id);
                if (role == null)
                    return new ResponseResult(ResponseResultEnum.Error, $"Cannot find role with id:{id}");

                _context.UserRoles.Remove(role);
                await _context.SaveChangesAsync();

                return new ResponseResult(ResponseResultEnum.Success, "Role created successfully");
            }
            catch (Exception ex)
            {
                LoggingService.LogException(ex);
                return new ResponseResult(ResponseResultEnum.Error, $"Error occurs when creating role: {ex.Message}");
            }
        }

        public async Task<IEnumerable<UserRole>> GetAllAsync()
        {
            return await _context.UserRoles.ToListAsync();
        }

        public async Task<UserRole> GetByIdAsync(int id)
        {
            return await _context.UserRoles.FirstOrDefaultAsync(u => u.RoleID == id);
        }

        public async Task<IEnumerable<UserRole>> GetByIdsAsync(IEnumerable<int> ids)
        {
            if (ids == null || !ids.Any())
                return new List<UserRole>();

            var roles = await _context.UserRoles.Where(u => ids.Contains(u.RoleID)).ToListAsync();

            return roles;
        }

        public async Task<IEnumerable<UserRole>> GetByNameAsync(string name)
        {
            return await _context.UserRoles.Where(u => u.RoleName == name).ToListAsync();
        }

        public async Task<ResponseResult> GetRolesByUserId(int id)
        {
            try
            {
                string sqlQuery = @"SELECT r.* 
                            FROM tblUserRoles r
                            INNER JOIN tblUsers u ON u.RoleId = r.RoleId
                            WHERE u.UserId = @UserId";

                var parameters = new SqlParameter("@UserId", id);

                List<UserRole> listRoles = await _context.UserRoles.FromSqlRaw(sqlQuery, parameters).ToListAsync();

                return new ResponseResult(ResponseResultEnum.Success, "Roles retrieved successfully", null, listRoles);
            }
            catch (Exception ex)
            {
                LoggingService.LogException(ex);
                return new ResponseResult(ResponseResultEnum.Error, $"Error occurs when getting roles by UserId: {ex.Message}");
            }
        }

        public async Task<ResponseResult> UpdateAsync(UserRole entity)
        {
            if (entity == null || entity.RoleID < 0)
                return new ResponseResult(ResponseResultEnum.Error, "Role can not be empty");

            try
            {
                _context.UserRoles.Update(entity);
                await _context.SaveChangesAsync();

                return new ResponseResult(ResponseResultEnum.Success, "Role updated successfully");
            }
            catch (Exception ex)
            {
                LoggingService.LogException(ex);
                return new ResponseResult(ResponseResultEnum.Error, $"Error occurs when updating role: {ex.Message}");
            }
        }
    }
}
