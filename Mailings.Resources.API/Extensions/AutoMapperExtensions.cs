using AutoMapper;
using Mailings.Resources.API.Configurations;

namespace Mailings.Resources.API.Extensions;
public static class AutoMapperExtensions
{
    public static void AddAutoMapper(this IServiceCollection services)
    {
        var mapper = AutoMapperConfiguration.CreateConfiguration()
            .CreateMapper();
        services.AddSingleton(mapper);
    }
}