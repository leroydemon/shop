using DbLevel.Models;
using FluentValidation;

namespace BussinessLogicLevel.FluentValidators
{
    public class BrandValidator : AbstractValidator<Brand>
    {
        public BrandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name can't be empty");
        }
    }
}
