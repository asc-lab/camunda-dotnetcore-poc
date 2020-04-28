using System;
using HeroesForHire.Domain;

namespace HeroesForHire.Controllers.Dtos
{
    public class OrderDto
    {
        public Guid OrderId { get; set; }
        
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        
        public string SuperCode { get; set; }
        public string SuperpowerName { get; set; }
        
        public DateTime OrderFrom { get; set; }
        public DateTime OrderTo { get; set; }
        
        public string Status { get; set; }
        
        public Guid? AssignedHeroId { get; set; }
        public string AssignedHeroName { get; set; }
        
        public static OrderDto FromEntity(Order order)
        {
            return new OrderDto
            {
                OrderId = order.Id.Value,
                CustomerCode = order.Customer.Code,
                CustomerName = order.Customer.Name,
                SuperCode = order.Superpower.Code,
                SuperpowerName = order.Superpower.Name,
                OrderFrom = order.Period.From,
                OrderTo = order.Period.To,
                Status = order.Status.ToString(),
                AssignedHeroId = order.Offer?.AssignedHero.Id.Value,
                AssignedHeroName = order.Offer?.AssignedHero.Name
            };
        }
    }
}