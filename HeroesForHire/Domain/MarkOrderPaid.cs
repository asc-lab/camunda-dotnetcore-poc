using System.Threading;
using System.Threading.Tasks;
using HeroesForHire.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HeroesForHire.Domain
{
    public class MarkOrderPaid
    {
        public class Command : IRequest<Unit>
        {
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
                var invoice = await db.Invoices.FirstAsync(i => i.Id == request.InvoiceId, cancellationToken);
                
                invoice.MarkPaid();
                
                //signal camunda that order is paid

                await db.SaveChangesAsync(cancellationToken);
                
                return Unit.Value;
            }
        }
    }
}