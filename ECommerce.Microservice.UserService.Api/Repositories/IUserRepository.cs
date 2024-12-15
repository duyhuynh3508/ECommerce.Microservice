using System.Security.Cryptography;
using System.Text;
using ECommerce.Microservice.SharedLibrary.IBaseRepository;
using ECommerce.Microservice.SharedLibrary.Logging;
using ECommerce.Microservice.SharedLibrary.Response;
using ECommerce.Microservice.UserService.Api.DatabaseDbContext;
using ECommerce.Microservice.UserService.Api.Entities;
using ECommerce.Microservice.UserService.Api.Mapping;
using ECommerce.Microservice.UserService.Api.Models.User;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Microservice.UserService.Api.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<ResponseResult> Login(LoginModel model);
        Task<ResponseResult> ForgotPassword(UserForgotPasswordModel model);
    }

    public class UserRepository : IUserRepository
    {
        private readonly UserDbContext _context;

        public UserRepository(UserDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseResult> CreateAsync(User entity)
        {
            if (entity == null)
                return new ResponseResult(ResponseResultEnum.Error, "User can not be empty");
            try
            {
                entity.PasswordHash = HashPassword(entity.PasswordHash);

                await _context.Users.AddAsync(entity);
                await _context.SaveChangesAsync();

                return new ResponseResult(ResponseResultEnum.Success, "User created successfully");
            }
            catch (Exception ex)
            {
                LoggingService.LogException(ex);
                return new ResponseResult(ResponseResultEnum.Error, $"Error occurs when creating user: {ex.Message}");
            }
        }

        public async Task<ResponseResult> DeleteAsync(int id)
        {
            if (id < 0)
                return new ResponseResult(ResponseResultEnum.Error, "UserId must be greater than 0");

            try
            {
                var user = await GetByIdAsync(id);
                if (user == null)
                    return new ResponseResult(ResponseResultEnum.Error, $"Cannot find user with id:{id}");

                _context.Users.Remove(user);
                await _context.SaveChangesAsync();

                return new ResponseResult(ResponseResultEnum.Success, "User created successfully");
            }
            catch (Exception ex)
            {
                LoggingService.LogException(ex);
                return new ResponseResult(ResponseResultEnum.Error, $"Error occurs when creating user: {ex.Message}");
            }
        }

        public async Task<ResponseResult> ForgotPassword(UserForgotPasswordModel model)
        {
            if (model == null)
                return new ResponseResult(ResponseResultEnum.Error);

            if (string.IsNullOrWhiteSpace(model.UserNameOrEmail))
                return new ResponseResult(ResponseResultEnum.Error, "Please input username or email");

            if (string.IsNullOrWhiteSpace(model.OldPassword))
                return new ResponseResult(ResponseResultEnum.Error, "Please input old password");

            if (string.IsNullOrWhiteSpace(model.NewPassword))
                return new ResponseResult(ResponseResultEnum.Error, "Please input new password");

            if (string.IsNullOrWhiteSpace(model.ConfirmPassword))
                return new ResponseResult(ResponseResultEnum.Error, "Please input confirm password");

            if (!model.NewPassword.Equals(model.ConfirmPassword))
                return new ResponseResult(ResponseResultEnum.Error, "New password and confirm password must be the same");

            try
            {
                var user = _context.Users.FirstOrDefault(u => (u.UserName == model.UserNameOrEmail || u.Email == model.UserNameOrEmail) 
                && u.PasswordHash == HashPassword(model.OldPassword));

                if (user == null)
                    return new ResponseResult(ResponseResultEnum.Error, "Cannot find user");

                string newPasswordHash = HashPassword(model.NewPassword);

                string sqlQuery = "UPDATE tblUsers SET PasswordHash = @PasswordHash WHERE UserId = @UserId";

                var parameters = new[]
                {
                    new SqlParameter("@PasswordHash", newPasswordHash),
                    new SqlParameter("@UserId", user.UserID)
                };

                await _context.Database.ExecuteSqlRawAsync(sqlQuery, parameters);

                return new ResponseResult(ResponseResultEnum.Success, "Password updated successfully");
            }
            catch (Exception ex)
            {
                LoggingService.LogException(ex);
                return new ResponseResult(ResponseResultEnum.Error, $"Error occurs when updating password: {ex.Message}");
            }
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserID == id);
        }

        public async Task<IEnumerable<User>> GetByIdsAsync(IEnumerable<int> ids)
        {
            if (ids == null || !ids.Any())
                return new List<User>();

            var users = await _context.Users.Where(u => ids.Contains(u.UserID)).ToListAsync();

            return users;
        }

        public async Task<IEnumerable<User>> GetByNameAsync(string name)
        {
            return await _context.Users.Where(u => u.FirstName.Contains(name) || u.LastName.Contains(name)).ToListAsync();
        }

        public async Task<ResponseResult> Login(LoginModel model)
        {
            if (model == null)
                return new ResponseResult(ResponseResultEnum.Error);

            if (string.IsNullOrWhiteSpace(model.UserNameOrEmail))
                return new ResponseResult(ResponseResultEnum.Error, "Please input username or email");

            if (string.IsNullOrWhiteSpace(model.Password))
                return new ResponseResult(ResponseResultEnum.Error, "Please input old password");

            try
            {
                var user = _context.Users.FirstOrDefault(u => (u.UserName == model.UserNameOrEmail || u.Email == model.UserNameOrEmail)
                && u.PasswordHash == HashPassword(model.Password));

                if (user == null)
                    return new ResponseResult(ResponseResultEnum.Error, "Cannot find user");

                return new ResponseResult(ResponseResultEnum.Success, "Login successfully", user);
            }
            catch (Exception ex)
            {
                LoggingService.LogException(ex);
                return new ResponseResult(ResponseResultEnum.Error, $"Error occurs when login: {ex.Message}");
            }
        }

        public Task<ResponseResult> Logout()
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseResult> UpdateAsync(User entity)
        {
            if (entity == null || entity.UserID < 0)
                return new ResponseResult(ResponseResultEnum.Error, "User can not be empty");

            try
            {
                _context.Users.Update(entity);
                await _context.SaveChangesAsync();

                return new ResponseResult(ResponseResultEnum.Success, "User updated successfully");
            }
            catch (Exception ex)
            {
                LoggingService.LogException(ex);
                return new ResponseResult(ResponseResultEnum.Error, $"Error occurs when updating user: {ex.Message}");
            }
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (var b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
