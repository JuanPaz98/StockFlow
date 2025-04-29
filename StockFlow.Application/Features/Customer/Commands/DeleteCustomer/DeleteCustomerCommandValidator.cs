using FluentValidation;

namespace StockFlow.Application.Features.Customer.Commands.DeleteCustomer
{
    public class DeleteCustomerCommandValidator : AbstractValidator<DeleteCustomerCommand>
    {
        public DeleteCustomerCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Id is required.")
                .GreaterThan(0)
                .WithMessage("Id must be greater than 0.");
        }
    }
}
