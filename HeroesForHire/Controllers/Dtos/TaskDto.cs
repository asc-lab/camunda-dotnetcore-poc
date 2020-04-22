using System;

namespace HeroesForHire.Controllers.Dtos
{
    public class TaskDto
    {
        public string TaskId { get; set; }
        
        public string Assignee { get; set; }
        
        public Guid? OrderId { get; set; }
        
        public string RequestedSuperpower { get; set; }
        
        public DateTime? OrderFrom { get; set; }
        
        public DateTime? OrderTo { get; set; }
        
        public string Customer { get; set; }
        
        public string OrderStatus { get; set; }
    }
}