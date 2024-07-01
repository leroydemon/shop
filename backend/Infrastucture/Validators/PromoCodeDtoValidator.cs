using DbLevel.Models;
using FluentValidation;

namespace Infrastucture.Validators
{
    public class PromoCodeDtoValidator : AbstractValidator<PromoCodeDto>
    {
        public PromoCodeDtoValidator()
        {
             RuleFor(x => x.Code)
               .NotEmpty();
             RuleFor(x => x.ExpireDate)
               .NotEmpty();
             RuleFor(x => x.AmountDiscoint)
               .NotEmpty();
        }
    }
}
