using AutoMapper;

namespace Mailings.Resources.Shared.Extensions;
public static class AutoMapperExtensions
{
    public static void CreateDoubleLinkedMap<T1, T2>(
        this IMapperConfigurationExpression mapper)
    {
        mapper.CreateMap<T1, T2>();
        mapper.CreateMap<T2, T1>();
    }
}