using System;
using FluentValidation;

namespace HeroesForHire.Controllers.Dtos
{
    public class NoHeroesForOrderDto
    {
        public string TaskId { get; set; }
        public Guid OrderId { get; set; }
    }
    
    public class NoHeroesForOrderDtoValidator : AbstractValidator<NoHeroesForOrderDto>
    {
        public NoHeroesForOrderDtoValidator()
        {
            RuleFor(x => x.TaskId).NotEmpty();
            RuleFor(x => x.OrderId).NotEmpty();
        }
    }
}