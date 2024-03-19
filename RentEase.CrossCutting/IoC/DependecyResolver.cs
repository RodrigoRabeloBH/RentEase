using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RentEase.Application.Services;
using RentEase.Domain.Interfaces.Repositories;
using RentEase.Domain.Interfaces.Services;
using RentEase.Infrastructure.Data;
using RentEase.Infrastructure.Repositories;
using System.Diagnostics.CodeAnalysis;

namespace RentEase.CrossCutting.IoC
{
    [ExcludeFromCodeCoverage]
    public static class DependecyResolver
    {
        public static IServiceCollection AddDependecyResolver(this IServiceCollection services, IConfiguration configuration)
        {
            RegisterRepositories(services);
            RegisterDatabases(services, configuration);
            RegisterServices(services);
            RegisterRabbitMq(services, configuration);

            return services;
        }

        private static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IPropertyServices, PropertyServices>();

            services.AddScoped<ICloudinaryServices, CloudinaryServices>();
        }

        private static void RegisterRepositories(IServiceCollection services)
        {
            services.AddScoped<IPropertyRepository, PropertyRepository>();
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        }

        private static void RegisterDatabases(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<RentEaseContext>(options =>
            {
                options.UseNpgsql(configuration["DB_CONNECTION"]);
            });
        }

        private static void RegisterRabbitMq(IServiceCollection services, IConfiguration configuration)
        {
            var username = configuration["RABBIT_USER"] ?? "guest";
            var password = configuration["RABBIT_PASSWORD"] ?? "guest";

            services.AddMassTransit(configure =>
            {
                configure.UsingRabbitMq((context, config) =>
                {
                    config.Host(configuration["RABBIT_HOST"], "/", host =>
                    {
                        host.Username(username);
                        host.Password(password);
                    });

                    config.ConfigureEndpoints(context);
                });

                configure.AddEntityFrameworkOutbox<RentEaseContext>(o =>
                {
                    o.QueryDelay = TimeSpan.FromSeconds(10);
                    o.UsePostgres();
                    o.UseBusOutbox();
                });
            });
        }
    }
}
