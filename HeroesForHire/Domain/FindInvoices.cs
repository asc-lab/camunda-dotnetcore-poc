using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HeroesForHire.Controllers.Dtos;
using HeroesForHire.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HeroesForHire.Domain
{
    public class FindInvoices
    {
        public class Query : IRequest<ICollection<InvoiceDto>>
        {
            public string CustomerCode { get; set; }
            public List<InvoiceStatus> Statuses { get; set; }
        }
        
        
        public class Handler : IRequestHandler<Query, ICollection<InvoiceDto>>
        {
            private readonly HeroesDbContext db;

            public Handler(HeroesDbContext db)
            {
                this.db = db;
            }

            public async Task<ICollection<InvoiceDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var query = db.Invoices.AsQueryable();

                if (request.CustomerCode != null)
                {
                    query = query.Where(i => i.Customer.Code == request.CustomerCode);
                }

                if (request.Statuses != null && request.Statuses.Count > 0)
                {
                    query = query.Where(i => request.Statuses.Contains(i.Status));
                }

                var matchingInvoices = await query.ToListAsync(cancellationToken);

                return matchingInvoices.Select(InvoiceDto.FromEntity).ToList();
            }
        }
    }
}