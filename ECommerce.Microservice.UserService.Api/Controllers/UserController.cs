using Azure;
using ECommerce.Microservice.SharedLibrary.Response;
using ECommerce.Microservice.UserService.Api.Enumerators;
using ECommerce.Microservice.UserService.Api.Models.User;
using ECommerce.Microservice.UserService.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Microservice.UserService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpGet("getUserById")]
        public async Task<IActionResult> GetUserByID(int id)
        {
            var response = await _service.GetUserById(id);

            if (response != null && response.responseResult == ResponseResultEnum.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [Authorize]
        [HttpGet("getUsersByIds")]
        public async Task<IActionResult> GetUsersByIds(IEnumerable<int> ids)
        {
            var response = await _service.GetUsersByIds(ids);

            if (response != null && response.responseResult == ResponseResultEnum.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [Authorize(Roles = UserRoleResources.Admin)]
        [HttpGet("getAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var response = await _service.GetAllUsers();

            if (response != null && response.responseResult == ResponseResultEnum.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [Authorize(Roles = UserRoleResources.Admin)]
        [HttpPost("createNewUser")]
        public async Task<IActionResult> CreateNewUser(UserCreateModel model)
        {
            var response = await _service.CreateNewUser(model);

            if (response != null && response.responseResult == ResponseResultEnum.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [Authorize]
        [HttpPost("updateUser")]
        public async Task<IActionResult> UpdateUser(UserUpdateModel model)
        {
            var currentUsername = User.Identity?.Name;
            bool isAdmin = User.IsInRole(UserRoleResources.Admin);
            bool isCurrentCustomer = currentUsername == model.UserName;

            if (isAdmin || isCurrentCustomer)
            {
                var response = await _service.UpdateUser(model);

                if (response != null && response.responseResult == ResponseResultEnum.Success)
                {
                    return Ok(response);
                }

                return BadRequest(response);
            }

            return Forbid();
        }

        [Authorize(Roles = UserRoleResources.Admin)]
        [HttpPost("deleteUser")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var response = await _service.DeleteUser(id);

            if (response != null && response.responseResult == ResponseResultEnum.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }
    }
}