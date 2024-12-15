using ECommerce.Microservice.SharedLibrary.BaseModel;
using ECommerce.Microservice.UserService.Api.Enumerators;

namespace ECommerce.Microservice.UserService.Api.Models.User
{
    public class UserUpdateModel : BaseModel
    {
        public int UserID {  get; set; }
        public UserRoleEnum RoleID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
