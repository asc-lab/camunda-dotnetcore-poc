using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HeroesForHire.Adapters.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HeroesForHire.Domain
{
    public class GetAllSuperPowers
    {
        public class Query : IRequest<ICollection<Superpower>>
        {
            
        }
        
        public class Handler : IRequestHandler<Query,ICollection<Superpower>>
        {
            private readonly HeroesDbContext db;

            public Handler(HeroesDbContext db)
            {
                this.db = db;
            }


            public async Task<ICollection<Superpower>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await db.Superpowers.ToListAsync(cancellationToken: cancellationToken);
            }
        }
    }
}