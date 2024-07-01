using DbLevel.Models;
using FluentValidation;

namespace Infrastucture.Validators
{
    public class CategoryDtoValidator : AbstractValidator<CategoryDto>
    {
        public CategoryDtoValidator()
        {
            RuleFor(x => x.Name)
              .NotEmpty();
        }
    }
}
