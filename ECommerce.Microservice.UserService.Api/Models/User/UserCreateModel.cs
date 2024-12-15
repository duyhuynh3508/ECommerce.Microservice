using ECommerce.Microservice.SharedLibrary.BaseModel;
using ECommerce.Microservice.UserService.Api.Enumerators;

namespace ECommerce.Microservice.UserService.Api.Models.User
{
    public class UserCreateModel : BaseModel
    {
        public UserRoleEnum RoleID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
