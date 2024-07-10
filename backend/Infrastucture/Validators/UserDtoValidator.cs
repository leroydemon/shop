using FluentValidation;
using Infrastucture.DtoModels;

namespace Infrastucture.Validators
{
    public class UserDtoValidator : AbstractValidator<UserDto>
    {
        public UserDtoValidator()
        {
            RuleFor(x => x.UserName)
                .Length(5);
        }
    }
}
