using System.Collections.Generic;
using System.Data;
using ECommerce.Microservice.SharedLibrary.Response;
using ECommerce.Microservice.UserService.Api.Entities;
using ECommerce.Microservice.UserService.Api.Mapping;
using ECommerce.Microservice.UserService.Api.Models.User;
using ECommerce.Microservice.UserService.Api.Models.UserRole;
using ECommerce.Microservice.UserService.Api.Repositories;

namespace ECommerce.Microservice.UserService.Api.Services
{
    public interface IUserRoleService
    {
        Task<ResponseResult> GetRoleById(int id);
        Task<ResponseResult> GetRolesByIds(IEnumerable<int> ids);
        Task<ResponseResult> GetRolesByUserId(int userId);
        Task<ResponseResult> GetAllRoles();
        Task<ResponseResult> CreateNewRole(UserRoleModel model);
        Task<ResponseResult> UpdateRole(UserRoleModel model);
        Task<ResponseResult> DeleteRole(int id);
    }

    public class UserRoleService : IUserRoleService
    {
        private readonly IUserRoleRepository _repository;
        private readonly IUserRoleMapping _mapping;

        public UserRoleService(IUserRoleRepository repository, IUserRoleMapping mapping)
        {
            _repository = repository;
            _mapping = mapping;
        }

        public async Task<ResponseResult> GetRoleById(int id)
        {
            var role = await _repository.GetByIdAsync(id);

            if (role == null)
                return new ResponseResult(ResponseResultEnum.Error, "Cannot find role!");

            var roleModel = (UserRoleModel)_mapping.ToModel(role);
            return new ResponseResult(ResponseResultEnum.Success, "", roleModel);
        }

        public async Task<ResponseResult> GetRolesByIds(IEnumerable<int> ids)
        {
            var roles = await _repository.GetByIdsAsync(ids);
            List<UserRoleModel> roleModels = new List<UserRoleModel>();

            if (roles == null || !roles.Any())
                return new ResponseResult(ResponseResultEnum.Error, "Roles are empty");

            foreach (var role in roles)
            {
                var model = (UserRoleModel)_mapping.ToModel(role);
                roleModels.Add(model);
            }

            return new ResponseResult(ResponseResultEnum.Success, "", null, roleModels);
        }

        public async Task<ResponseResult> GetRolesByUserId(int userId)
        {
            var roles = await _repository.GetRolesByUserId(userId);
            List<UserRoleModel> roleModels = new List<UserRoleModel>();

            if (roles == null || roles.collection == null || !roles.collection.Any())
                return new ResponseResult(ResponseResultEnum.Error, "Roles are empty for the user");

            var listRoles = (List<UserRole>) roles.collection;

            foreach (var role in listRoles)
            {
                var model = (UserRoleModel)_mapping.ToModel(role);
                roleModels.Add(model);
            }

            return new ResponseResult(ResponseResultEnum.Success, "", null, roleModels);
        }

        public async Task<ResponseResult> GetAllRoles()
        {
            var roles = await _repository.GetAllAsync();
            List<UserRoleModel> roleModels = new List<UserRoleModel>();

            if (roles == null || !roles.Any())
                return new ResponseResult(ResponseResultEnum.Error, "Roles are empty");

            foreach (var role in roles)
            {
                var model = (UserRoleModel)_mapping.ToModel(role);
                roleModels.Add(model);
            }

            return new ResponseResult(ResponseResultEnum.Success, "", null, roleModels);
        }

        public async Task<ResponseResult> CreateNewRole(UserRoleModel model)
        {
            var role = (UserRole)_mapping.ToEntity(model);

            var responseResult = await _repository.CreateAsync(role);

            return responseResult;
        }

        public async Task<ResponseResult> UpdateRole(UserRoleModel model)
        {
            if (model == null)
                return new ResponseResult(ResponseResultEnum.Error, "Cannot find role!");

            var roleToUpdate = await _repository.GetByIdAsync(model.RoleID);

            if (roleToUpdate == null)
                return new ResponseResult(ResponseResultEnum.Error, "Cannot find role to update!");

            roleToUpdate = (UserRole)_mapping.ToEntity(roleToUpdate, model);

            var responseResult = await _repository.UpdateAsync(roleToUpdate);

            return responseResult;
        }

        public async Task<ResponseResult> DeleteRole(int id)
        {
            var responseResult = await _repository.DeleteAsync(id);

            return responseResult;
        }
    }
}
