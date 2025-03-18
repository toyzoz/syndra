using FluentValidation;
using Ordering.Application.Commands.Cancel;

namespace Ordering.Application.Validations
{
    public class CancelOrderCommandValidator
       : AbstractValidator<CancelOrderCommand>
    {
        public CancelOrderCommandValidator()
        {

        }
    }
}
