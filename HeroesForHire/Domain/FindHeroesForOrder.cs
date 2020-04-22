using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HeroesForHire.Controllers.Dtos;
using HeroesForHire.DataAccess;
using MediatR;

namespace HeroesForHire.Domain
{
    public class FindHeroesForOrder
    {
        public class Query : IRequest<ICollection<HeroDto>>
        {
            public OrderId OrderId { get; set; }
        }

        public class Handler : IRequestHandler<Query, ICollection<HeroDto>>
        {
            private readonly HeroesDbContext db;

            public Handler(HeroesDbContext db)
            {
                this.db = db;
            }

            public async Task<ICollection<HeroDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var order = db.Orders.FirstOrDefault(o => o.Id == request.OrderId);
                var availableHeroes = await db.FindHeroForOrder(order);

                return availableHeroes.Select(HeroDto.FromEntity).ToList();
            }
        }
    }
}