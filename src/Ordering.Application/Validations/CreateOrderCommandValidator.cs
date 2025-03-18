using FluentValidation;
using Microsoft.Extensions.Logging;
using Ordering.Application.Commands.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Validations
{
    public class CreateOrderCommandValidator
        : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.Items).Must(x => x.Count != 0).WithMessage("No items in order");
        }
    }
}
