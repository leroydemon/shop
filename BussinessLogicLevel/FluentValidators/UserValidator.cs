

using DbLevel.Models;
using FluentValidation;

namespace BussinessLogicLevel.FluentValidators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name can't be empty");
            RuleFor(x => x.Login).NotEmpty().WithMessage("Login can't be empty");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password can't be empty");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Password can't be empty");
        }
    }
}
