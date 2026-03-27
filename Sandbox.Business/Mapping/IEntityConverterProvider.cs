namespace Sandbox.Business.Mapping;

public interface IEntityConverterProvider
{
    IEntityConverter<TEntity, TModel> GetConverter<TEntity, TModel>()
        where TEntity : class
        where TModel : class;
}
