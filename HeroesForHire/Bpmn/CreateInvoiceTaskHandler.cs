using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Camunda.Worker;
using HeroesForHire.Domain;
using MediatR;

namespace HeroesForHire
{
    [HandlerTopics("Topic_CreateInvoice", LockDuration = 10_000)]
    public class CreateInvoiceTaskHandler : ExternalTaskHandler
    {
        private readonly IMediator bus;

        public CreateInvoiceTaskHandler(IMediator bus)
        {
            this.bus = bus;
        }

        public override async Task<IExecutionResult> Process(ExternalTask externalTask)
        {
            var invoiceId = await bus.Send(new CreateInvoice.Command
            {
                OrderId = new OrderId(Guid.Parse(externalTask.Variables["orderId"].AsString()))
            });

            return new CompleteResult
            {
                Variables = new Dictionary<string, Variable>
                {
                    ["invoiceId"] = new Variable(invoiceId.Value.ToString(), VariableType.String)
                }
            };
        }
    }
}