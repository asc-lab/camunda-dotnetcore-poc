using System;
using FluentValidation;

namespace HeroesForHire.Controllers.Dtos
{
    public class RejectOfferDto
    {
        public string TaskId { get; set; }
        
        public Guid OrderId { get; set; }
    }
    
    public class RejectOfferDtoValidator : AbstractValidator<RejectOfferDto>
    {
        public RejectOfferDtoValidator()
        {
            RuleFor(x => x.TaskId).NotEmpty();
            RuleFor(x => x.OrderId).NotEmpty();
        }
    }
}