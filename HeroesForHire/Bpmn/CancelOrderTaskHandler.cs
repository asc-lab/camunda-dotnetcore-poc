using System;
using System.Threading.Tasks;
using Camunda.Worker;
using HeroesForHire.Domain;
using MediatR;

namespace HeroesForHire
{
    [HandlerTopics("Topic_CancelOrder", LockDuration = 10_000)]
    public class CancelOrderTaskHandler : ExternalTaskHandler
    {
        private readonly IMediator bus;

        public CancelOrderTaskHandler(IMediator bus)
        {
            this.bus = bus;
        }

        public override async Task<IExecutionResult> Process(ExternalTask externalTask)
        {
            await bus.Send(new CancelOrder.Command
            {
                OrderId = new OrderId(Guid.Parse(externalTask.Variables["orderId"].AsString())),
                InvoiceId = new InvoiceId(Guid.Parse(externalTask.Variables["invoiceId"].AsString()))
            });
            
            return new CompleteResult{};
        }
    }
}