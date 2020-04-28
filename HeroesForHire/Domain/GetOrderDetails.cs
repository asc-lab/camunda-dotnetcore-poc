using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HeroesForHire.Controllers.Dtos;
using HeroesForHire.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HeroesForHire.Domain
{
    public class GetOrderDetails
    {
        public class Query : IRequest<OrderDto>
        {
            public OrderId OrderId { get; set; }
        }

        public class Handler : IRequestHandler<Query, OrderDto>
        {
            private readonly HeroesDbContext db;

            public Handler(HeroesDbContext db)
            {
                this.db = db;
            }

            public async Task<OrderDto> Handle(Query request, CancellationToken cancellationToken)
            {
                var order = await db.Orders.FirstAsync(o => o.Id == request.OrderId, cancellationToken);
                
                return OrderDto.FromEntity(order);
            }
        }

    }
}