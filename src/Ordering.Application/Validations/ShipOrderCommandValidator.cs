using FluentValidation;
using Ordering.Application.Commands.Ship;

namespace Ordering.Application.Validations
{
    public class ShipOrderCommandValidator
       : AbstractValidator<ShipOrderCommand>
    {
        public ShipOrderCommandValidator()
        {

        }
    }
}
