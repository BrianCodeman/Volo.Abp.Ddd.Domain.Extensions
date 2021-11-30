using System.Threading.Tasks;
using Volo.Abp.DynamicProxy;
using Volo.Abp.ObjectMapping;

namespace Volo.Abp.Ddd.Domain.Extensions.Interceptors
{
    public class CheckableEntityInterceptor : AbpInterceptor
    {
        private readonly IObjectMapper _mapper;

        public CheckableEntityInterceptor(IObjectMapper mapper)
        {
            _mapper = mapper;
        }

        public override async Task InterceptAsync(IAbpMethodInvocation invocation)
        {
            await invocation.ProceedAsync();
            if (!invocation.Method.IsSpecialName && !invocation.Method.Name.StartsWith("Check"))
            {
                (invocation.TargetObject as ICheckableEntity)?.Check();
                _mapper.Map(invocation.TargetObject.GetType().BaseType, invocation.TargetObject.GetType().BaseType,
                    invocation.TargetObject, (invocation.TargetObject as IProxiedEntity)?.Entity);
            }
        }
    }
}