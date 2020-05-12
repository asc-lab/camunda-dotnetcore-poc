using System.Collections.Generic;
using System.Threading.Tasks;
using HeroesForHire.Controllers.Dtos;
using HeroesForHire.Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HeroesForHire.Controllers
{
    [Authorize(Roles = "Sales")]
    [ApiController]
    [Route("[controller]")]
    public class SalesmanController : ControllerBase
    {
        private readonly IMediator bus;

        public SalesmanController(IMediator bus)
        {
            this.bus = bus;
        }
        
        [HttpGet("PendingInvoices")]
        public async Task<ICollection<InvoiceDto>> PendingInvoices()
        {
            return await bus.Send(new FindInvoices.Query
            {
                Statuses = new List<InvoiceStatus> { InvoiceStatus.NEW }
            });
        }

        [HttpGet("MyTasks")]
        public async Task<ICollection<TaskDto>> MyTasks()
        {
            var tasks = await bus.Send(new GetSalesmanTasks.Query
            {
                SalesmanLogin = User.Identity.Name
            });
            return tasks;
        }

        [HttpPost("MyTasks/{taskId}/claim")]
        public async Task<TaskDto> ClaimTask([FromRoute] string taskId)
        {
            return await bus.Send(new ClaimTask.Command
            {
                TaskId = taskId,
                UserLogin = User.Identity.Name
            });
        }
    }
}