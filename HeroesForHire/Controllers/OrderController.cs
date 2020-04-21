using System.Threading.Tasks;
using HeroesForHire.Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HeroesForHire.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IMediator bus;

        public OrderController(IMediator bus)
        {
            this.bus = bus;
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder([FromBody] PlaceOrder.Command request)
        {
            await bus.Send(request);
            return Ok();
        }
    }
}