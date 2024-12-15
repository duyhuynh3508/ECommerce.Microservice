using ECommerce.Microservice.SharedLibrary.BaseModel;

namespace ECommerce.Microservice.UserService.Api.Models.User
{
    public class UserForgotPasswordModel : BaseModel
    {
        public string UserNameOrEmail { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
