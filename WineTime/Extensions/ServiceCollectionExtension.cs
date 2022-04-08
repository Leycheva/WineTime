namespace Microsoft.Extensions.DependencyInjection
{
    using Microsoft.EntityFrameworkCore;
    using WineTime.Core.Constants;
    using WineTime.Core.Contracts;
    using WineTime.Core.Services;
    using WineTime.Extensions;
    using WineTime.Infrastructure.Data;
    using WineTime.Infrastructure.Data.Repositories;

    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            var provider = services.AddScoped<IApplicatioDbRepository, ApplicatioDbRepository>().BuildServiceProvider();
            services.AddScoped<IProductsService, ProductsService >().BuildServiceProvider();
            services.AddScoped<IDegustationsService, DegustationsService>().BuildServiceProvider();
            services.AddScoped<IManufacturesService, ManufacturesService>().BuildServiceProvider();
            services.AddScoped<IRegionsService, RegionsService >().BuildServiceProvider();
            services.AddScoped<IFavoritesService, FavoritesService >().BuildServiceProvider();

            var scope = provider.CreateScope();

            var data = scope.ServiceProvider.GetService<ApplicationDbContext>();
            var seeder = new DataSeeder(data);
            seeder.Seed();

            seeder.SeedAdmin(scope.ServiceProvider);

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
