using System;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Ddd.Domain.Extensions.Entities;
using Volo.Abp.ObjectMapping;

namespace Volo.Abp.Ddd.Domain.Extensions.Factories
{
    public class EntityFactory : IEntityFactory
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IObjectMapper _mapper;

        public EntityFactory(IServiceProvider serviceProvider, IObjectMapper mapper)
        {
            _serviceProvider = serviceProvider;
            _mapper = mapper;
        }

        public TEntity GetFromDto<TEntity, TCreateDto>(TCreateDto dto) where TEntity : CheckableEntity
        {
            var entity = _serviceProvider.GetService<TEntity>();
            _mapper.Map(dto, entity);
            ((ICheckableEntity)entity)?.Check();
            return entity;
        }
    }
}