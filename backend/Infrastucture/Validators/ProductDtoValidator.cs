﻿using FluentValidation;
using Infrastucture.DtoModels;

namespace Infrastucture.Validators
{
    public class ProductDtoValidator : AbstractValidator<ProductDto>
    {
        public ProductDtoValidator()
        {
            RuleFor(x => x.Name)
               .NotEmpty();
            RuleFor(x => x.UnitPrice)
               .NotEmpty();
            RuleFor(x => x.BrandId)
              .NotEmpty();
        }
    }
}
