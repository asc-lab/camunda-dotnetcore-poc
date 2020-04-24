using System;

namespace HeroesForHire.Controllers.Dtos
{
    public class AcceptOfferDto
    {
        public string TaskId { get; set; }
        
        public Guid OrderId { get; set; }
    }
}