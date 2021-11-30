using System;
using System.Collections.Generic;
using AutoMapper;

namespace Volo.Abp.Ddd.Domain.Extensions
{
    public class EntitiesAopAutoMapperProfile : Profile
    {
        internal static List<Type> Types { get; } = new(); 
        
        public EntitiesAopAutoMapperProfile()
        {
            foreach (Type type in Types)
            {
                CreateMap(type, type);
            }
        }
    }
}
