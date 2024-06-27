using DbLevel.Models;
using FluentValidation;
using System.Text.Json;

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

            RuleFor(cart => cart.ProductListJson)
                .Must(ValidJson).WithMessage("Product List JSON must be a valid JSON string.");
        }

        private bool ValidJson(string productListJson)
        {
            try
            {
                var productList = JsonSerializer.Deserialize<Dictionary<Guid, int>>(productListJson);
                return productList != null;
            }
            catch
            {
                return false;
            }
        }
    }
}
