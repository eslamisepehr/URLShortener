using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using URLShortener.Domain;
using URLShortener.Infrastructure.Database;
using URLShortener.Infrastructure.Domain;

namespace URLShortener.Infrastructure
{
    public static class ConfigureInfrastructure
    {
        public static IServiceCollection RegisterInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("AppDbContext")));

            services.RegisterRepositories();

            return services;
        }

        private static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IShortUrlRepository, ShortUrlRepository>();
            services.Decorate<IShortUrlRepository, CachedShortUrlRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
