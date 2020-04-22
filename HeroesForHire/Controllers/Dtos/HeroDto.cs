using System;
using HeroesForHire.Domain;

namespace HeroesForHire.Controllers.Dtos
{
    public class HeroDto
    {
        public Guid HeroId { get; set; }
        
        public string Name { get; set; }

        public static HeroDto FromEntity(Hero hero)
        {
            return new HeroDto
            {
                HeroId = hero.Id.Value,
                Name = hero.Name
            };
        }
    }
}