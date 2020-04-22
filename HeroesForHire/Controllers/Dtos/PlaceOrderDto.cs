using System;

namespace HeroesForHire.Controllers.Dtos
{
    public class PlaceOrderDto
    {
        public string SuperpowerCode { get; set; }
        public DateTime OrderFrom { get; set; }
        public DateTime OrderTo { get; set; }
    }
}