using Volo.Abp.Ddd.Domain.Extensions.Entities;
using Volo.Abp.DependencyInjection;

namespace Volo.Abp.Ddd.Domain.Extensions.Factories
{
    public interface IEntityFactory : ITransientDependency
    {
        TEntity GetFromDto<TEntity, TCreateDto>(TCreateDto dto) where TEntity : CheckableEntity;
    }
}