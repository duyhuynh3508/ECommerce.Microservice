using ECommerce.Microservice.SharedLibrary.BaseModel;

namespace ECommerce.Microservice.UserService.Api.Models.UserRole
{
    public class UserRoleModel : BaseModel
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; }
    }
}
