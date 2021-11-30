using System;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities;

namespace Volo.Abp.Ddd.Domain.Extensions.Entities
{
    [Serializable]
    public abstract class CheckableEntity : Entity, ICheckableEntity, IProxiedEntity
    {
        [NotMapped] public IProxiedEntity Entity { get; set; } = default;

        public abstract void Check();

    }
    
    [Serializable]
    public abstract class CheckableEntity<TKey> : CheckableEntity, IEntity<TKey>
    {
        public virtual TKey Id { get; protected set; }

        protected CheckableEntity()
        {
        }

        protected CheckableEntity(TKey id)
        {
            Id = id;
        }

        public override object[] GetKeys()
        {
            return new object[] {Id};
        }

        public override string ToString()
        {
            return $"[ENTITY: {GetType().Name}] Id = {Id}";
        }
    }
}