using DbLevel.Models;
using FluentValidation;

namespace BussinessLogicLevel.FluentValidators
{
    public class StorageValidator : AbstractValidator<Storage>
    {
        public StorageValidator()
        {
            RuleFor(x => x.Phone).NotEmpty().WithMessage("Phone can't be empty").Matches(@"^\d{10}$").WithMessage("Number must be 10 digits");
            RuleFor(x => x.City).NotEmpty().WithMessage("City can't be empty");
            RuleFor(x => x.Address).NotEmpty().WithMessage("Address can't be empty");
        }
    }
}
