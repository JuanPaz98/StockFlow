using FluentValidation;

namespace StockFlow.Application.Features.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(x => x.Data.CustomerId)
                .NotEmpty()
                .WithMessage("Customer ID is required.")
                .GreaterThan(0)
                .WithMessage("Customer ID must be greater than 0.");

            RuleFor(x => x.Data.TotalAmount)
                .NotEmpty()
                .WithMessage("Total amount is required.");
            
            RuleFor(x => x.Data.Status)
                .NotEmpty()
                .WithMessage("Status is required.");
            
            RuleFor(x => x.Data.PaidAmount)
                .NotEmpty()
                .WithMessage("Paid amount is required.");
        }
    }
}
