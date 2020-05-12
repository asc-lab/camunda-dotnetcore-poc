using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HeroesForHire.DataAccess
{
    public static class EntityFrameworkInstaller
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection services, string cnnString)
        {
            services.AddDbContext<HeroesDbContext>(options =>
            {
                options.UseNpgsql(cnnString);
            });

            return services;
        }
    }
}