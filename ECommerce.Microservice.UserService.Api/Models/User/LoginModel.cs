using ECommerce.Microservice.SharedLibrary.BaseModel;

namespace ECommerce.Microservice.UserService.Api.Models.User
{
    public class LoginModel : BaseModel
    {
        public string UserNameOrEmail { get; set; }
        public string Password { get; set; }
    }
}
