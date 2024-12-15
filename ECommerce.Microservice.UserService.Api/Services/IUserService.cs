using ECommerce.Microservice.SharedLibrary.Response;
using ECommerce.Microservice.UserService.Api.Entities;
using ECommerce.Microservice.UserService.Api.Mapping;
using ECommerce.Microservice.UserService.Api.Models.User;
using ECommerce.Microservice.UserService.Api.Repositories;

namespace ECommerce.Microservice.UserService.Api.Services
{
    public interface IUserService
    {
        Task<ResponseResult> GetUserById(int id);
        Task<ResponseResult> GetUsersByIds(IEnumerable<int> ids);
        Task<ResponseResult> GetAllUsers();
        Task<ResponseResult> CreateNewUser(UserCreateModel model);
        Task<ResponseResult> UpdateUser(UserUpdateModel model);
        Task<ResponseResult> DeleteUser(int id);
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IUserMapping _mapping;

        public UserService(IUserRepository repository, IUserMapping mapping)
        {
            _repository = repository;
            _mapping = mapping;
        }

        public async Task<ResponseResult> CreateNewUser(UserCreateModel model)
        {
            var user = (User)_mapping.ToEntity(model);

            var responseResult = await _repository.CreateAsync(user);

            return responseResult;
        }

        public async Task<ResponseResult> DeleteUser(int id)
        {
            var responseResult = await _repository.DeleteAsync(id);

            return responseResult;
        }

        public async Task<ResponseResult> GetAllUsers()
        {
            var users = await _repository.GetAllAsync();
            List<UserModel> userModels = new List<UserModel>();

            if (users == null || !users.Any())
                return new ResponseResult(ResponseResultEnum.Error, "Users are empty");

            foreach (var item in users)
            {
                var model = (UserModel)_mapping.ToModel(item);
                userModels.Add(model);
            }

            return new ResponseResult(ResponseResultEnum.Success, "", null, userModels);
        }

        public async Task<ResponseResult> GetUserById(int id)
        {
            var user = await _repository.GetByIdAsync(id);

            if (user == null) return new ResponseResult(ResponseResultEnum.Error, "Cannot find user!");

            var userModel = (UserModel)_mapping.ToModel(user);

            return new ResponseResult(ResponseResultEnum.Success, "", userModel);
        }

        public async Task<ResponseResult> GetUsersByIds(IEnumerable<int> ids)
        {
            var users = await _repository.GetByIdsAsync(ids);
            List<UserModel> userModels = new List<UserModel>();

            if (users == null || !users.Any())
                return new ResponseResult(ResponseResultEnum.Error, "Users are empty");

            foreach (var item in users)
            {
                var model = (UserModel)_mapping.ToModel(item);
                userModels.Add(model);
            }

            return new ResponseResult(ResponseResultEnum.Success, "", null, userModels);
        }

        public async Task<ResponseResult> UpdateUser(UserUpdateModel model)
        {
            if (model == null)
                return new ResponseResult(ResponseResultEnum.Error, "Can not find user!");

            var userToUpdate = await _repository.GetByIdAsync(model.UserID);

            if (userToUpdate == null) return new ResponseResult(ResponseResultEnum.Error, "Can not find product!");

            userToUpdate = (User)_mapping.ToEntity(userToUpdate, model);

            var responseResult = await _repository.UpdateAsync(userToUpdate);

            return responseResult;
        }
    }
}
