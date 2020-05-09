using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Camunda.Worker;
using HeroesForHire.Domain;
using MediatR;

namespace HeroesForHire
{
    [HandlerTopics("Topic_NotifyCustomer_NoHeroes", LockDuration = 10_000)]
    public class NotifyCustomerTaskHandler : ExternalTaskHandler
    {
        private readonly IMediator bus;

        public NotifyCustomerTaskHandler(IMediator bus)
        {
            this.bus = bus;
        }

        public override async Task<IExecutionResult> Process(ExternalTask externalTask)
        {
            await bus.Send(new NotifyCustomer.Command
            {
                OrderId = new OrderId(Guid.Parse(externalTask.Variables["orderId"].AsString()))
            });
            
            
            return new CompleteResult {};
        }
    }
}