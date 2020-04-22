using System;
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
    [Microsoft.AspNetCore.Components.Route("[controller]")]
    public class HeroesController : ControllerBase
    {
        private readonly IMediator bus;

        public HeroesController(IMediator bus)
        {
            this.bus = bus;
        }

        [HttpGet("AvailableForOrder/{orderId}")]
        public async Task<ICollection<HeroDto>> FindAvailableHeroes([FromRoute] Guid orderId)
        {
            return await bus.Send(new FindHeroesForOrder.Query
            {
                OrderId = new OrderId(orderId)
            });
        }
    }
}