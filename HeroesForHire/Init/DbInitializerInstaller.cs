using Microsoft.Extensions.DependencyInjection;

namespace HeroesForHire.Init
{
    public static class DbInitializerInstaller
    {
        public static IServiceCollection AddDbInitializer(this IServiceCollection services)
        {
            services.AddScoped<DbSeed>();
            services.AddHostedService<DbInitializer>();
            return services;
        }
    }
}