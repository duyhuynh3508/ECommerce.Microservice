using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Microservice.SharedLibrary.Mapping
{
    public interface IBaseMapping<TEntity, TModel>
    {
        TEntity ToEntity(TModel model);
        TEntity ToEntity(TEntity entity, TModel model);
        TModel ToModel(TEntity entity);
    }
}
