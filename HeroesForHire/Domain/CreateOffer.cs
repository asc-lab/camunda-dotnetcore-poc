using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HeroesForHire.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HeroesForHire.Domain
{
    public class CreateOffer
    {
        public class Command : IRequest<Unit>
        {
            public string TaskId { get; set; }
            public OrderId OrderId { get; set; }
            public HeroId SelectedHero { get; set; }
        }

        public class Handler : IRequestHandler<Command,Unit>
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
                var order = await db.Orders.FirstAsync(o => o.Id == request.OrderId, cancellationToken);
                var candidateHero = await db.Heroes.FirstAsync(h => h.Id == request.SelectedHero, cancellationToken);
                
                //ensure hero available
                var heroAvailable = (await db.FindHeroForOrder(order))
                    .Any(h => h.Id == candidateHero.Id);
                if (!heroAvailable)
                {
                    //throw biz error
                }
                
                //create offer and lock hero
                order.CreateOfferWithHero(candidateHero);

                await db.SaveChangesAsync(cancellationToken);
                
                //change order status & complete task
                await bpmnService.CompleteTask(request.TaskId, order);
                
                
                
                throw new System.NotImplementedException();
            }
        }
        
    }
}