using ECommerce.Microservice.SharedLibrary.BaseEntity;
using ECommerce.Microservice.SharedLibrary.BaseModel;
using ECommerce.Microservice.SharedLibrary.Mapping;
using ECommerce.Microservice.UserService.Api.Entities;
using ECommerce.Microservice.UserService.Api.Models.UserRole;

namespace ECommerce.Microservice.UserService.Api.Mapping
{
    public interface IUserRoleMapping : IBaseMapping<BaseEntity, BaseModel>
    {
    }

    public class UserRoleMapping : IUserRoleMapping
    {
        public BaseEntity ToEntity(BaseModel model)
        {
            BaseEntity? entity = null;
            if (model is UserRoleModel roleModel)
            {
                entity = new UserRole()
                {
                    RoleID = roleModel.RoleID,
                    RoleName = roleModel.RoleName
                };
            }
            return entity;
        }

        public BaseEntity ToEntity(BaseEntity entity, BaseModel model)
        {
            if (entity is UserRole role && model is UserRoleModel userRoleModel)
            {
                role.RoleID = userRoleModel.RoleID;
                role.RoleName = userRoleModel.RoleName;
            }

            return entity;
        }

        public BaseModel ToModel(BaseEntity entity)
        {
            BaseModel? model = null;
            if (entity is UserRole role)
            {
                model = new UserRoleModel()
                {
                    RoleID = role.RoleID,
                    RoleName = role.RoleName
                };
            }

            return model;
        }
    }
}
