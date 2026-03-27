using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.Business.Mapping
{
    public interface IEntityConverter<TEntity, TModel>
    {
        public TModel? ToModel(TEntity? entity);
        public TEntity? ToEntity(TModel? model);
        public IEnumerable<TModel?> ToModels(IEnumerable<TEntity?> entities);
        public IEnumerable<TEntity?> ToEntities(IEnumerable<TModel?> models);
    }
}
