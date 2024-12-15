using ECommerce.Microservice.SharedLibrary.Response;
using ECommerce.Microservice.UserService.Api.Models.User;
using ECommerce.Microservice.UserService.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Microservice.UserService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        public AuthController(IAuthService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var response = await _authService.Login(model);

            if (response != null && response.responseResult == ResponseResultEnum.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserCreateModel model)
        {
            var response = await _userService.CreateNewUser(model);

            if (response != null && response.responseResult == ResponseResultEnum.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpPost("forgotPassword")]
        public async Task<IActionResult> ForgotPassword(UserForgotPasswordModel model)
        {
            var response = await _authService.ForgotPassword(model);

            if (response != null && response.responseResult == ResponseResultEnum.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] string token)
        {
            if (string.IsNullOrEmpty(token))
                return BadRequest();

            var response = _authService.Logout(token);

            return Ok(response);
        }

        [HttpPost("refreshToken")]
        public async Task<IActionResult> RefreshToken([FromBody] string oldToken)
        {
            var response = await _authService.RefreshToken(oldToken);

            if (response != null && response.responseResult == ResponseResultEnum.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }
    }
}
