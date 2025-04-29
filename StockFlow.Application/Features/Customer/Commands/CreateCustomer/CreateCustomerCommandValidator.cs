using FluentValidation;

namespace StockFlow.Application.Features.Customer.Commands.CreateCustomer
{
    public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerCommandValidator()
        {
            RuleFor(x => x.Data.Name)
                .NotEmpty().WithMessage("Name is obligatory.")
                .NotNull().WithMessage("Name can't be null")
                .MinimumLength(3).WithMessage("This field must has at least 3 characters");

            RuleFor(x => x.Data.Email)
              .EmailAddress().WithMessage("Email is not valid.")
              .MaximumLength(100).WithMessage("The email address cannot be more than 100 characters.");

            RuleFor(x => x.Data.Phone)
                .Matches(@"^\d{10}$").WithMessage("Phone must have 10 digits.")
                .When(x => !string.IsNullOrEmpty(x.Data.Phone));
        }
    }
}
