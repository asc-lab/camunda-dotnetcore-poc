using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace HeroesForHire
{
    public class BpmnProcessDeployService : IHostedService
    {
        private readonly BpmnService bpmnService;

        public BpmnProcessDeployService(BpmnService bpmnService)
        {
            this.bpmnService = bpmnService;
        }


        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await bpmnService.DeployProcessDefinition();
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}