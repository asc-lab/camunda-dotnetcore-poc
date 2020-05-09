using System.Threading;
using System.Threading.Tasks;
using HeroesForHire.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HeroesForHire.Domain
{
    public class CreateInvoice
    {
        public class Command : IRequest<Unit>
        {
            public OrderId OrderId { get; set; }
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
                var order = await db.Orders.FirstAsync(o => o.Id == request.OrderId, cancellationToken);

                var invoice = Invoice.ForOrder(order);

                db.Invoices.Add(invoice);

                await db.SaveChangesAsync(cancellationToken);
                
                return Unit.Value;
            }
        }
    }
}