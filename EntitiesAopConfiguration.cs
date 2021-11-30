using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace Volo.Abp.Ddd.Domain.Extensions
{
    public static class EntitiesAopConfiguration
    {
        public static void Configure<TModule>(ServiceConfigurationContext context) where TModule : AbpModule
        {
            var assembly = typeof(TModule).Assembly;
            var entities = assembly.GetTypes().Where(o => o.GetInterface(nameof(IProxiedEntity)) is not null);
            
            foreach (var entity in entities)
            {
                context.Services.AddTransient(entity);
                EntitiesAopAutoMapperProfile.Types.Add(entity);
            }
        }
    }
}