using DbLevel.Models;
using FluentValidation;

namespace BussinessLogicLevel.FluentValidators
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name can't be empty");
        }
    }
}
