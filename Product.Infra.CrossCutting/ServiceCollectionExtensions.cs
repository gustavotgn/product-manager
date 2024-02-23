using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Product.Infra.Data;
using Product.Service.Services;
using System.Linq;

namespace Product.Infra.CrossCutting.IOC
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            var implementations = typeof(ApplicationDbContext).Assembly
            .GetTypes().Where(x => x.Namespace != null && x.BaseType != null && x.BaseType.Name == "BaseRepository`2").ToList();

            foreach (var implementation in implementations)
            {
                services.AddScoped(implementation);
            }
            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            var implementations = typeof(BaseService).Assembly
            .GetTypes().Where(x => x.Namespace != null && x.BaseType != null && x.BaseType.Name == "BaseService").ToList();

            foreach (var implementation in implementations)
            {
                services.AddScoped(implementation);
            }
            return services;
        }

        public static IApplicationBuilder ApplyMigration(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var _db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            if (_db != null && _db.Database.GetPendingMigrations().Any())
                _db.Database.Migrate();

            return app;
        }
    }
}