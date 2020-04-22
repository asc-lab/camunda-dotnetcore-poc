using HeroesForHire.Domain;

namespace HeroesForHire.Controllers.Dtos
{
    public class SuperpowerDto
    {
        public string Code { get; set; }
        
        public string Name { get; set; }

        public static SuperpowerDto FromEntity(Superpower superpower)
        {
            return new SuperpowerDto
            {
                Code = superpower.Code,
                Name = superpower.Name
            };
        }
    }
}