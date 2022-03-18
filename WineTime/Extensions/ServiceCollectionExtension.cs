using Microsoft.EntityFrameworkCore;
using WineTime.Extensions;
using WineTime.Infrastructure.Data;
using WineTime.Infrastructure.Data.Repositories;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            var provider = services.AddScoped<IApplicatioDbRepository, ApplicatioDbRepository>().BuildServiceProvider();

            var scope = provider.CreateScope();

            var data = scope.ServiceProvider.GetService<ApplicationDbContext>();
            var seeder = new DataSeeder(data);
            seeder.Seed();

            return services;
        }

        public static IServiceCollection AddApplicationDbContexts(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            services.AddDatabaseDeveloperPageExceptionFilter();

            return services;
        }

    }
}
