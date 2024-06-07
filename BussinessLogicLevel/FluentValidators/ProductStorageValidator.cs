using DbLevel.Models;
using FluentValidation;

namespace BussinessLogicLevel.FluentValidators
{
    public class ProductStorageValidator : AbstractValidator<ProductStorage>
    {
        public ProductStorageValidator()
        {
            RuleFor(x => x.Quantity).NotEmpty().WithMessage("Quantity can't be empty");
            RuleFor(x => x.ProductId).NotEmpty().WithMessage("Invalid product");
        }
    }
}
