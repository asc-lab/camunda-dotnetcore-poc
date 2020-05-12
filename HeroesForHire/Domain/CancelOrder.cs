using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using HeroesForHire.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HeroesForHire.Domain
{
    public class CancelOrder
    {
        public class Command : IRequest<Unit>
        {
            public OrderId OrderId { get; set; }
            public InvoiceId InvoiceId { get; set; }
        }

        public class Handler : IRequestHandler<Command, Unit>
        {
            private readonly HeroesDbContext db;

            public Handler(HeroesDbContext db)
            {
                this.db = db;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
                
                var order = await db.Orders.FirstAsync(o => o.Id == request.OrderId, cancellationToken);
                var invoice = await db.Invoices.FirstAsync(i => i.Id == request.InvoiceId, cancellationToken);

                order.Cancel();
                invoice.Cancel();
                
                await db.SaveChangesAsync(cancellationToken);
                    
                tx.Complete();
                return Unit.Value;
            }
        }
    }
}