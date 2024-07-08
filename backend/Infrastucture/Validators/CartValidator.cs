using DbLevel.Models;
using FluentValidation;

namespace Infrastucture.Validators
{
    public class CartValidator : AbstractValidator<Cart>
    {
        public CartValidator()
        {
            RuleFor(cart => cart.Id)
                .NotEmpty().WithMessage("Cart ID is required.");

            RuleFor(cart => cart.UserId)
                .NotEmpty().WithMessage("User ID is required.");

            RuleFor(cart => cart.TotalPrice)
                .GreaterThanOrEqualTo(0).WithMessage("Total Price must be greater than or equal to 0.");

            RuleFor(cart => cart.ProductAmount)
                .GreaterThanOrEqualTo(0).WithMessage("Product Amount must be greater than or equal to 0.");
        }
    }
}
