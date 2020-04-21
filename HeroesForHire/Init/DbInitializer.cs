using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HeroesForHire.Init
{
    public class DbInitializer : IHostedService
    {
        private readonly IServiceProvider serviceProvider;

        public DbInitializer(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = serviceProvider.CreateScope();
            await scope.ServiceProvider.GetService<DbSeed>().SeedData();
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}