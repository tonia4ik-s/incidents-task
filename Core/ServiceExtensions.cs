using AutoMapper;
using Core.Interfaces;
using Core.Profiles;
using Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Core;

public static class ServiceExtensions
{
    public static void AddCustomServices(this IServiceCollection services)
    {
        services.AddScoped<IContactService, ContactService>();
     
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IIncidentService, IncidentService>();
    }
    
    public static void AddAutoMapper(this IServiceCollection services)
    {
        var configures = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new ContactProfile());
            mc.AddProfile(new AccountProfile());
            mc.AddProfile(new IncidentProfile());
        });

        var mapper = configures.CreateMapper();
        services.AddSingleton(mapper);
    }
}
