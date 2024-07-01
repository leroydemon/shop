using DbLevel.Models;
using FluentValidation;

namespace Infrastucture.Validators
{
    public class OrderDtoValidator : AbstractValidator<OrderDto>
    {
        public OrderDtoValidator()
        {
        }
    }
}
