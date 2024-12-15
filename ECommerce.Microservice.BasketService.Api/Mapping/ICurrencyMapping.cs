using ECommerce.Microservice.ProductService.Api.Entities;
using ECommerce.Microservice.SharedLibrary.BaseEntity;
using ECommerce.Microservice.SharedLibrary.BaseModel;
using ECommerce.Microservice.SharedLibrary.Mapping;
using ECommerce.Microservice.ProductService.Api.Models.Currency;

namespace ECommerce.Microservice.ProductService.Api.Mapping
{
    public interface ICurrencyMapping : IBaseMapping<BaseEntity, BaseModel>
    {
    }
    public class CurrencyMapping : ICurrencyMapping
    {
        public BaseEntity ToEntity(BaseModel model)
        {
            BaseEntity? entity = null;

            if (model is CurrencyModel currencyModel)
            {
                entity = new BasketItem()
                {
                    CurrencyID = currencyModel.CurrencyID,
                    CurrencyName = currencyModel.CurrencyName
                };
            }

            return entity;
        }

        public BaseEntity ToEntity(BaseEntity entity, BaseModel model)
        {
            if (entity is BasketItem currency)
            {
                if (model is CurrencyModel currencyUpdateModel)
                {
                    currency.CurrencyID = currencyUpdateModel.CurrencyID;
                    currency.CurrencyName = currencyUpdateModel.CurrencyName;
                }
            }

            return entity;
        }

        public BaseModel ToModel(BaseEntity entity)
        {
            BaseModel model = null;

            if (entity is BasketItem currency)
            {
                model = new CurrencyModel()
                {
                    CurrencyID = currency.CurrencyID,
                    CurrencyName = currency.CurrencyName
                };
            }

            return model;
        }
    }
}
