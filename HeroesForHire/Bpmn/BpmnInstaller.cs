using System;
using Camunda.Worker;
using Camunda.Worker.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace HeroesForHire
{
    public static class BpmnInstaller
    {
        public static IServiceCollection AddCamunda(this IServiceCollection services)
        {
            services.AddSingleton<BpmnService>();
            services.AddHostedService<BpmnProcessDeployService>();
            
            services.AddCamundaWorker(options =>
            {
                options.BaseUri = new Uri("http://localhost:8080/rest/engine/default");
                options.WorkerCount = 1;
            })
            .AddHandler<NotifyCustomerTaskHandler>()
            .AddHandler<CreateInvoiceTaskHandler>();
            
            return services;
        }
    }
}