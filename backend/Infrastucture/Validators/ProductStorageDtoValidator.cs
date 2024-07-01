using DbLevel.Models;
using FluentValidation;

namespace Infrastucture.Validators
{
    public class ProductStorageDtoValidator : AbstractValidator<ProductStorageDto>
    {
        public ProductStorageDtoValidator()
        {
            RuleFor(x => x.Quantity)
                .NotEmpty();
            RuleFor(x => x.ProductId)
                .NotEmpty();
            RuleFor(x => x.StorageId)
                .NotEmpty();
        }
    }
}
