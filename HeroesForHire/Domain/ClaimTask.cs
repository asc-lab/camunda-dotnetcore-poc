using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using HeroesForHire.Controllers.Dtos;
using HeroesForHire.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HeroesForHire.Domain
{
    public class ClaimTask
    {
        public class Command : IRequest<TaskDto>
        {
            public string TaskId { get; set; }
            public string UserLogin { get; set; }
        }

        public class Handler : IRequestHandler<Command, TaskDto>
        {
            private readonly BpmnService bpmnService;
            private readonly HeroesDbContext db;

            public Handler(BpmnService bpmnService, HeroesDbContext db)
            {
                this.bpmnService = bpmnService;
                this.db = db;
            }

            public async Task<TaskDto> Handle(Command request, CancellationToken cancellationToken)
            {
                using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
                
                var claimedTask = await bpmnService.ClaimTask(request.TaskId, request.UserLogin);

                var order = await db.Orders
                    .FirstOrDefaultAsync(o => o.ProcessInstanceId == claimedTask.ProcessInstanceId, cancellationToken);

                var orderDto = TaskDto.FromEntity(claimedTask, order);
                
                tx.Complete();
                
                return orderDto;
            }
        }
    }
}