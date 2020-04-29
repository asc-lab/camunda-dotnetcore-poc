using System;
using FluentValidation;

namespace HeroesForHire.Controllers.Dtos
{
    public class PlaceOrderDto
    {
        public string SuperpowerCode { get; set; }
        public DateTime OrderFrom { get; set; }
        public DateTime OrderTo { get; set; }
    }
    
    public class PlaceOrderDtoValidator : AbstractValidator<PlaceOrderDto>
    {
        public PlaceOrderDtoValidator()
        {
            RuleFor(x => x.SuperpowerCode).NotEmpty();
            RuleFor(x => x.OrderFrom).NotEmpty();
            RuleFor(x => x.OrderTo).NotEmpty();
        }
    }
}