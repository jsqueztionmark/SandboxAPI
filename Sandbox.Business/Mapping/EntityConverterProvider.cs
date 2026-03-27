using Microsoft.Extensions.DependencyInjection;

namespace Sandbox.Business.Mapping;

public class EntityConverterProvider : IEntityConverterProvider
{
    private readonly IServiceProvider _serviceProvider;

    public EntityConverterProvider(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IEntityConverter<TEntity, TModel> GetConverter<TEntity, TModel>()
        where TEntity : class
        where TModel : class
    {
        return _serviceProvider.GetRequiredService<IEntityConverter<TEntity, TModel>>();
    }
}
