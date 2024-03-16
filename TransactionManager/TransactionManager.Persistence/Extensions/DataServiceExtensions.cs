using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace TransactionManager.Persistence.Extensions;

public static class DataServiceExtensions
{
    public static IServiceCollection AddDataServices(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<DataContext>(opt => { opt.UseSqlServer(connectionString); });

        services.AddSingleton<DapperContext>(provider => new DapperContext(connectionString));

        return services;
    }
}