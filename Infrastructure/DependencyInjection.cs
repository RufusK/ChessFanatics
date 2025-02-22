using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        ConfigurationManager config)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            var connectionString = config.GetConnectionString("Database");
            options.UseNpgsql(connectionString);
        });
        
        return services;
    }
}