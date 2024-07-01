using DbLevel.Models;
using FluentValidation;

namespace Infrastucture.Validators
{
    public class StorageDtoValidator : AbstractValidator<StorageDto>
    {
        public StorageDtoValidator()
        {
            RuleFor(x => x.Address)
                .NotEmpty();
            RuleFor(x => x.Phone)
                .NotEmpty();
            RuleFor(x => x.City)
                .NotEmpty();
        }
    }
}
