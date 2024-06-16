
using DbLevel.Models;
using FluentValidation;

namespace Infrastucture.Validators
{
    public class CategoryDtoValidator : AbstractValidator<Category>
    {
        public CategoryDtoValidator()
        {
            RuleFor(x => x.Name)
                .Length(5);
        }
    }
}
