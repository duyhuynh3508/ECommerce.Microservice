using ECommerce.Microservice.SharedLibrary.BaseEntity;
using ECommerce.Microservice.SharedLibrary.BaseModel;
using ECommerce.Microservice.SharedLibrary.Mapping;
using ECommerce.Microservice.UserService.Api.Entities;
using ECommerce.Microservice.UserService.Api.Enumerators;
using ECommerce.Microservice.UserService.Api.Models.User;

namespace ECommerce.Microservice.UserService.Api.Mapping
{
    public interface IUserMapping : IBaseMapping<BaseEntity, BaseModel>
    {
    }

    public class UserMapping : IUserMapping
    {
        public BaseEntity ToEntity(BaseModel model)
        {
            BaseEntity? entity = null;

            if (model is UserModel userModel)
            {
                entity = new User()
                {
                    UserID = userModel.UserID,
                    RoleID = (int)userModel.RoleID,
                    UserName = userModel.UserName,
                    Email = userModel.Email,
                    FirstName = userModel.FirstName,
                    LastName = userModel.LastName
                };
            }
            else if (model is UserCreateModel userCreateModel) 
            {
                entity = new User()
                {
                    RoleID = (int)userCreateModel.RoleID,
                    UserName = userCreateModel.UserName,
                    PasswordHash = userCreateModel.Password,
                    Email = userCreateModel.Email,
                    FirstName = userCreateModel.FirstName,
                    LastName = userCreateModel.LastName
                };
            }

            return entity;
        }

        public BaseEntity ToEntity(BaseEntity entity, BaseModel model)
        {
            if (entity is User user && model is UserUpdateModel userUpdateModel)
            {
                user.UserID = userUpdateModel.UserID;
                user.RoleID = (int)userUpdateModel.RoleID;
                user.UserName = userUpdateModel.UserName;
                user.Email = userUpdateModel.Email;
                user.FirstName = userUpdateModel.FirstName;
                user.LastName = userUpdateModel.LastName;
            }

            return entity;
        }

        public BaseModel ToModel(BaseEntity entity)
        {
            BaseModel? model = null;
            if (entity is User user)
            {
                model = new UserModel()
                {
                    UserID = user.UserID,
                    RoleID = (UserRoleEnum)user.RoleID,
                    UserName = user.UserName,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName
                };
            }

            return model;
        }
    }
}
