using System.Collections.Generic;
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
    public class ClientController : ControllerBase
    {
        private readonly IMediator bus;

        public ClientController(IMediator bus)
        {
            this.bus = bus;
        }

        [HttpGet("Orders")]
        public async Task<List<OrderDto>> ClientOrders()
        {
            return await bus.Send(new GetCustomerOrders.Query
            {
                CustomerCode = User.CompanyCode()
            });
        }
    }
}