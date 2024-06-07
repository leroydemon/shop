using DbLevel.Models;
using FluentValidation;

namespace BussinessLogicLevel.FluentValidators
{
    public class CartValidator : AbstractValidator<Cart>
    {
        public CartValidator()
        {
            RuleFor(x => x.UnitPrice).NotEmpty().WithMessage("UnitPrice can't be empty");
        }
    }
}
