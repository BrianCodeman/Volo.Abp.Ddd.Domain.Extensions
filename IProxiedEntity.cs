using Volo.Abp.Domain.Entities;

namespace Volo.Abp.Ddd.Domain.Extensions
{
    public interface IProxiedEntity
    {
        IProxiedEntity Entity { get; set; }
    }
}