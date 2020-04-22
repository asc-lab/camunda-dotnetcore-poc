using System;
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
    public class GetAllSuperPowers
    {
        public class Query : IRequest<ICollection<SuperpowerDto>>
        {
            
        }
        
        public class Handler : IRequestHandler<Query,ICollection<SuperpowerDto>>
        {
            private readonly HeroesDbContext db;

            public Handler(HeroesDbContext db)
            {
                this.db = db;
            }


            public async Task<ICollection<SuperpowerDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var powers = await db.Superpowers
                    .ToListAsync(cancellationToken: cancellationToken);

                return powers.Select(p => SuperpowerDto.FromEntity(p)).ToList();
            }
        }
    }
}