using Infrastructure.Data;
using Infrastructure.Interfaces;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class ServiceExtensions
{
    public static void AddCustomServices(this IServiceCollection services)
    {
        services.AddScoped<IContactService, ContactService>();
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IIncidentService, IncidentService>();
    }
    public static void AddDbContext(this IServiceCollection service, string connectionString)
    {
        service.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
    }
}
