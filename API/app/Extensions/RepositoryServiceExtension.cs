using app.Data;
using app.Data.Implements;
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

            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<ISectionRepository, SectionRepository>();
            services.AddScoped<IForumRepository, ForumRepository>();
            services.AddScoped<ITopicRepository, TopicRepostiory>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<ITokenRepository, TokenRepository>();
            
            services.AddScoped<IRepositoryManager, RepositoryManager>();

            return services;
        }

    }
}