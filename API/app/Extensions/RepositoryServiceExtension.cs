using app.Data;
using app.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace app.Extensions
{
    public static class RepositoryServiceExtension
    {

        public static IServiceCollection AddRepositoryService(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<RepositoryContext>(options =>
                options.UseNpgsql(config.GetConnectionString("PgsqlConnection")));

            services.AddScoped<IRepositoryManager, RepositoryManager>();

            return services;
        }

    }
}