using DbLevel.Models;
using FluentValidation;

namespace Infrastucture.Validators
{
    public class BrandDtoValidator : AbstractValidator<BrandDto>
    {
        public BrandDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .Length(5);
        }
    }
}
