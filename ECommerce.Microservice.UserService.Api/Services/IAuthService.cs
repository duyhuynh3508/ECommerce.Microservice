using System.Collections.Generic;
using ECommerce.Microservice.SharedLibrary.Response;
using ECommerce.Microservice.UserService.Api.Entities;
using ECommerce.Microservice.UserService.Api.JwtHelper;
using ECommerce.Microservice.UserService.Api.Mapping;
using ECommerce.Microservice.UserService.Api.Models.User;
using ECommerce.Microservice.UserService.Api.Repositories;

namespace ECommerce.Microservice.UserService.Api.Services
{
    public interface IAuthService
    {
        Task<ResponseResult> Login(LoginModel model);
        Task<ResponseResult> Logout(string token);
        Task<ResponseResult> ForgotPassword(UserForgotPasswordModel model);
        Task<ResponseResult> RefreshToken(string oldToken);
    }

    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserRoleRepository _roleRepository;
        private readonly JwtHandler _jwtHelper;
        public AuthService(IUserRepository repository, IUserRoleRepository roleRepository, JwtHandler jwtHelper)
        {
            _userRepository = repository;
            _roleRepository = roleRepository;
            _jwtHelper = jwtHelper;
        }

        public async Task<ResponseResult> ForgotPassword(UserForgotPasswordModel model)
        {
            var responseResult = await _userRepository.ForgotPassword(model);

            return responseResult;
        }

        public async Task<ResponseResult> Login(LoginModel model)
        {
            var responseResult = await _userRepository.Login(model);

            if (responseResult != null && responseResult.data != null)
            {
                var user = (User)responseResult.data;
                var roleResponseResult = await _roleRepository.GetRolesByUserId(user.UserID);

                if (roleResponseResult != null && roleResponseResult.collection != null && roleResponseResult.collection.Any())
                {
                    List<UserRole> roles = (List<UserRole>)roleResponseResult.collection;

                    var token = _jwtHelper.GenerateToken(user, roles);

                    return new ResponseResult(ResponseResultEnum.Success, token);
                }
            }

            return new ResponseResult(ResponseResultEnum.Error, "Login failed!");
        }

        public async Task<ResponseResult> Logout(string token)
        {
            _jwtHelper.RevokeToken(token);
            return new ResponseResult(ResponseResultEnum.Success, $"Token removed: {token}");
        }

        public async Task<ResponseResult> RefreshToken(string oldToken)
        {
            if (_jwtHelper.ValidateToken(oldToken))
            {
                var newToken = _jwtHelper.RefreshToken(oldToken);
                return new ResponseResult(ResponseResultEnum.Success, newToken);
            }

            return new ResponseResult(ResponseResultEnum.Error, "Invalid or expired token");
        }
    }
}
