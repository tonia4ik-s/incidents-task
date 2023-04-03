using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class ServiceExtensions
{
    public static void AddDbContext(this IServiceCollection service, string connectionString)
    {
        service.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
    }
    
    public static void AddRepositories(this IServiceCollection service)
    {
        service.AddScoped(typeof(IRepository<>), typeof(Repository<>));
    }
}
