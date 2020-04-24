using System;

namespace HeroesForHire.Controllers.Dtos
{
    public class CreateOfferDto
    {
        public string TaskId { get; set; }
        public Guid OrderId { get; set; }
        public Guid SelectedHero { get; set; }
    }
}