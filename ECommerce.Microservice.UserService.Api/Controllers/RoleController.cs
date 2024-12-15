using ECommerce.Microservice.SharedLibrary.Response;
using ECommerce.Microservice.UserService.Api.Enumerators;
using ECommerce.Microservice.UserService.Api.Models.UserRole;
using ECommerce.Microservice.UserService.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Microservice.UserService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly IUserRoleService _roleService;

        public RoleController(IUserRoleService roleService)
        {
            _roleService = roleService;
        }

        [Authorize(Roles = UserRoleResources.Admin)]
        [HttpGet("getRoleById")]
        public async Task<IActionResult> GetRoleById(int id)
        {
            var response = await _roleService.GetRoleById(id);

            if (response != null && response.responseResult == ResponseResultEnum.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [Authorize(Roles = UserRoleResources.Admin)]
        [HttpGet("getRolesByIds")]
        public async Task<IActionResult> GetRolesByIds(IEnumerable<int> ids)
        {
            var response = await _roleService.GetRolesByIds(ids);

            if (response != null && response.responseResult == ResponseResultEnum.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [Authorize(Roles = UserRoleResources.Admin)]
        [HttpGet("getRolesByUserId")]
        public async Task<IActionResult> GetRolesByUserId(int userId)
        {
            var response = await _roleService.GetRolesByUserId(userId);

            if (response != null && response.responseResult == ResponseResultEnum.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [Authorize(Roles = UserRoleResources.Admin)]
        [HttpGet("getAllRoles")]
        public async Task<IActionResult> GetAllRoles()
        {
            var response = await _roleService.GetAllRoles();

            if (response != null && response.responseResult == ResponseResultEnum.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [Authorize(Roles = UserRoleResources.Admin)]
        [HttpPost("createNewRole")]
        public async Task<IActionResult> CreateNewRole(UserRoleModel model)
        {
            var response = await _roleService.CreateNewRole(model);

            if (response != null && response.responseResult == ResponseResultEnum.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [Authorize(Roles = UserRoleResources.Admin)]
        [HttpPost("updateRole")]
        public async Task<IActionResult> UpdateRole(UserRoleModel model)
        {
            var response = await _roleService.UpdateRole(model);

            if (response != null && response.responseResult == ResponseResultEnum.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [Authorize(Roles = UserRoleResources.Admin)]
        [HttpPost("deleteRole")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            var response = await _roleService.DeleteRole(id);

            if (response != null && response.responseResult == ResponseResultEnum.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }
    }
}
