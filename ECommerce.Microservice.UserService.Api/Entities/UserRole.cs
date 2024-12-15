using System.ComponentModel.DataAnnotations;
using ECommerce.Microservice.SharedLibrary.BaseEntity;

namespace ECommerce.Microservice.UserService.Api.Entities
{
    public class UserRole : BaseEntity
    {
        [Key]
        public int RoleID { get; set; }
        public string RoleName { get; set; }
    }
}
