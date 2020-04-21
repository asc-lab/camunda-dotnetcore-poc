using Microsoft.Extensions.DependencyInjection;

namespace HeroesForHire
{
    public static class BpmnInstaller
    {
        public static IServiceCollection AddCamunda(this IServiceCollection services)
        {
            services.AddSingleton<BpmnService>();
            services.AddHostedService<BpmnProcessDeployService>();
            return services;
        }
    }
}