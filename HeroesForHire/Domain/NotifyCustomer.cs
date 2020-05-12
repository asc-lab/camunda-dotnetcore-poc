using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using Camunda.Api.Client.UserTask;
using HeroesForHire.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HeroesForHire.Domain
{
    public class NotifyCustomer
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
                using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
                
                var order = await db.Orders.FirstOrDefaultAsync(o => o.Id == request.OrderId, cancellationToken);

                var (notificationText,targetGroup) = order.Status switch
                {
                    OrderStatus.NoHeroesAvailable =>
                        ($"Your order {order.Id.Value} cannot be fulfilled because there are no heroes available",order.Customer.Code),
                    OrderStatus.Accepted => 
                        ($"Order {order.Id.Value} has been accepted","Sales"),
                    OrderStatus.Rejected => 
                        ($"Your order {order.Id.Value} has been rejected","Sales"),
                    OrderStatus.OfferCreated => 
                        ($"Offer for order {order.Id.Value} has been created",order.Customer.Code),
                    _ => 
                        ("","")
                };

                db.Notifications.Add(new Notification(notificationText, targetGroup, null));

                await db.SaveChangesAsync(cancellationToken);
                
                tx.Complete();
                
                return Unit.Value;
            }
        }
    }
}