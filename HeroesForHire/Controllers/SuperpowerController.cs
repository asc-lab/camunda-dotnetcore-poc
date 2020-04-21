using System.Collections.Generic;
using System.Threading.Tasks;
using HeroesForHire.Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HeroesForHire.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class SuperpowerController : ControllerBase
    {
        private readonly IMediator bus;

        public SuperpowerController(IMediator bus)
        {
            this.bus = bus;
        }

        [HttpGet]
        public async Task<ICollection<Superpower>> AllSuperpowers()
        {
            return await bus.Send(new GetAllSuperPowers.Query());
        }
    }
}