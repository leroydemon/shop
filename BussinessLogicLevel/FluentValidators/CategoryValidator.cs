
using DbLevel.Models;
using FluentValidation;

namespace BussinessLogicLevel.FluentValidators
{
    public class CategoryValidator : AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name can't be empty");
        }
    }
}
