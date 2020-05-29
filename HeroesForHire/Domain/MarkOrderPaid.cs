using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
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
            private readonly BpmnService bpmnService;

            public Handler(HeroesDbContext db, BpmnService bpmnService)
            {
                this.db = db;
                this.bpmnService = bpmnService;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
                var invoice = await db.Invoices.FirstAsync(i => i.Id == request.InvoiceId, cancellationToken);
                invoice.MarkPaid();
                await db.SaveChangesAsync(cancellationToken);
                await bpmnService.SendMessageInvoicePaid(invoice.Order);
                tx.Complete();
                return Unit.Value;
            }
        }
    }
}