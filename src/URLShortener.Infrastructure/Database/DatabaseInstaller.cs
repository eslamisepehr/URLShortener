using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace URLShortener.Infrastructure.Database
{
    public static class DatabaseInstaller
    {
        public static void InitializeDatabase(this IServiceScope serviceScope)
        {
            var dbContext = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
            dbContext.Database.EnsureCreated();
            dbContext.Database.Migrate();
        }
    }
}
