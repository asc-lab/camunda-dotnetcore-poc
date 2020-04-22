using System.Threading.Tasks;
using HeroesForHire.Controllers.Dtos;
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
        public async Task<IActionResult> PlaceOrder([FromBody] PlaceOrderDto request)
        {
            await bus.Send(new PlaceOrder.Command
            {
                CustomerCode = User.CompanyCode(),
                SuperpowerCode = request.SuperpowerCode,
                OrderFrom = request.OrderFrom,
                OrderTo = request.OrderTo
            });
            return Ok();
        }
    }
}