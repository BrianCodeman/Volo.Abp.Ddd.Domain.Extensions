using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.DynamicProxy;
using Volo.Abp.ObjectMapping;

namespace Volo.Abp.Ddd.Domain.Extensions.Interceptors
{
    public class RepositoryInterceptor : AbpInterceptor
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IObjectMapper _mapper;

        public RepositoryInterceptor(IServiceProvider serviceProvider, IObjectMapper mapper)
        {
            _serviceProvider = serviceProvider;
            _mapper = mapper;
        }

        public override async Task InterceptAsync(IAbpMethodInvocation invocation)
        {
            await invocation.ProceedAsync();
            var returnValue = invocation.ReturnValue;

            if (returnValue.GetType().Namespace == "Castle.Proxies")
            {
                return;
            }
            
            if (returnValue is IProxiedEntity proxiedEntity)
            {
                var aopEntity = GetAopEntity(proxiedEntity);
                invocation.ReturnValue = aopEntity;
            }

            if (returnValue is IEnumerable<IProxiedEntity> entities)
            {
                Type type = returnValue.GetType();
                foreach (var entity in entities.ToArray())
                {
                    var aopEntity = GetAopEntity(entity);
                    type.GetMethod("Remove")?.Invoke(returnValue, new object[] { entity });
                    type.GetMethod("Add")?.Invoke(returnValue, new[] { aopEntity });
                }
            }
            
            object GetAopEntity(IProxiedEntity entity)
            {
                var aopEntity = _serviceProvider.GetService(entity.GetType());
                _mapper.Map(entity.GetType(), entity.GetType(), entity, aopEntity);
                ((IProxiedEntity)aopEntity).Entity = entity;
                return aopEntity;
            }
        }
    }
}