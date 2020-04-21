using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HeroesForHire.Adapters.DataAccess;
using MediatR;
using Microsoft.AspNetCore.Mvc.Diagnostics;

namespace HeroesForHire.Domain
{
    public class PlaceOrder
    {
        public class Command : IRequest<Unit>
        {
            public string CustomerCode { get; set; }
            public string SuperpowerCode { get; set; }
            public DateTime OrderFrom { get; set; }
            public DateTime OrderTo { get; set; }
        }

        public class Handler : IRequestHandler<Command, Unit>
        {
            private readonly HeroesDbContext db;
            private readonly BpmnService bpmnService;

            public Handler(HeroesDbContext db, BpmnService bpmnService)
            {
                this.db = db;
                this.bpmnService = bpmnService;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var newOrder = new Order
                    (
                    db.Customers.FirstOrDefault(c=>c.Code==request.CustomerCode),
                    db.Superpowers.FirstOrDefault(s=>s.Code==request.SuperpowerCode),
                    DateRange.Between(request.OrderFrom,request.OrderTo)
                    );

                db.Orders.Add(newOrder);

                await db.SaveChangesAsync(cancellationToken);

                await bpmnService.StartProcessFor(newOrder);
                
                return Unit.Value;
            }
        }
    }
}