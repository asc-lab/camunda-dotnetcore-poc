using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HeroesForHire.DataAccess
{
    public static class EntityFrameworkInstaller
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection services)
        {
            //services.AddDbContext<HeroesDbContext>(options => { options.UseInMemoryDatabase("Heroes"); });
            services.AddDbContext<HeroesDbContext>(options =>
            {
                options.UseNpgsql("User ID=lab_user;Password=lab_pass;Database=camunda_poc_db;Host=localhost;Port=5435");
            });

            return services;
        }
    }
}