using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Ddd.Domain.Extensions.Entities;
using Volo.Abp.Ddd.Domain.Extensions.Factories;
using Volo.Abp.Ddd.Domain.Extensions.Interceptors;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.DynamicProxy;
using Volo.Abp.Modularity;

namespace Volo.Abp.Ddd.Domain.Extensions
{
    [DependsOn(typeof(AbpAutoMapperModule))]
    public class DomainExtensionsModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.OnRegistred(registration =>
            {
                if (typeof(CheckableEntity).IsAssignableFrom(registration.ImplementationType) &&
                    !DynamicProxyIgnoreTypes.Contains(registration.ImplementationType))
                {
                    registration.Interceptors.Add<CheckableEntityInterceptor>();
                }
                
                if (typeof(IRepository).IsAssignableFrom(registration.ImplementationType) &&
                    !DynamicProxyIgnoreTypes.Contains(registration.ImplementationType))
                {
                    registration.Interceptors.Add<RepositoryInterceptor>();
                }
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
           
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<DomainExtensionsModule>();
            });
            
            context.Services.AddTransient<CheckableEntityInterceptor>();
            context.Services.AddTransient<RepositoryInterceptor>();
        }
    }
}
