using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HeroesForHire.Adapters.DataAccess
{
    public static class EntityFrameworkInstaller
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection services)
        {
            services.AddDbContext<HeroesDbContext>(options => { options.UseInMemoryDatabase("Heroes"); });
            return services;
        }
    }
}