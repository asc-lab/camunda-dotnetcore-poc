using System;
using FluentValidation;

namespace HeroesForHire.Controllers.Dtos
{
    public class AcceptOfferDto
    {
        public string TaskId { get; set; }
        
        public Guid OrderId { get; set; }
    }
    
    public class AcceptOfferDtoValidator : AbstractValidator<AcceptOfferDto>
    {
        public AcceptOfferDtoValidator()
        {
            RuleFor(x => x.TaskId).NotEmpty();
            RuleFor(x => x.OrderId).NotEmpty();
        }
    }
}