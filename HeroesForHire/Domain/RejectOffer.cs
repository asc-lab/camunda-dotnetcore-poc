using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using HeroesForHire.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HeroesForHire.Domain
{
    public class RejectOffer
    {
        public class Command : IRequest<Unit>
        {
            public string TaskId { get; set; }
            public OrderId OrderId { get; set; }
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
                using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
                
                var order = await db.Orders
                    .Include(o=>o.Offer)
                    .ThenInclude(o => o.AssignedHero)
                    .Include(o => o.Customer)
                    .FirstAsync(o=>o.Id==request.OrderId,cancellationToken);

                order.RejectOffer();
                
                await db.SaveChangesAsync(cancellationToken);

                await bpmnService.CompleteTask(request.TaskId, order);
                
                tx.Complete();
                
                return Unit.Value;
            }
        }
    }
}