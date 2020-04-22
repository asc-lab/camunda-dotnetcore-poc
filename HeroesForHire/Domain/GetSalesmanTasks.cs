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
    public class GetSalesmanTasks
    {
        public class Query : IRequest<ICollection<TaskDto>>
        {
            public string SalesmanLogin { get; set; }
        }
        
        public class Handler : IRequestHandler<Query, ICollection<TaskDto>>
        {
            private readonly BpmnService bpmnService;
            private readonly HeroesDbContext db;

            public Handler(HeroesDbContext db, BpmnService bpmnService)
            {
                this.db = db;
                this.bpmnService = bpmnService;
            }

            public async Task<ICollection<TaskDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var tasks = await bpmnService.GetTasksFor(null, null);
                var processIds = tasks.Select(t => t.ProcessInstanceId).ToList();

                var orders = await db.Orders.Where(o => processIds.Contains(o.ProcessInstanceId))
                    .ToListAsync(cancellationToken: cancellationToken);
                var processIdToOrderMap = orders.ToDictionary(o => o.ProcessInstanceId, o => o);

                return (from task in tasks
                    let relatedOrder = processIdToOrderMap[task.ProcessInstanceId]
                    select new TaskDto
                    {
                        TaskId = task.Id,
                        Assignee = task.Assignee,
                        OrderId = relatedOrder.Id.Value,
                        RequestedSuperpower = relatedOrder?.Superpower.Code,
                        OrderFrom = relatedOrder?.Period.From,
                        OrderTo = relatedOrder?.Period.To,
                        Customer = relatedOrder?.Customer.Name,
                        OrderStatus = relatedOrder?.Status.ToString()
                    }).ToList();
            }
        }
    }
}