using FluentValidation;

namespace StockFlow.Application.Features.Payments.Commands.CreatePayment
{
    public class CreatePaymentCommandValidator : AbstractValidator<CreatePaymentCommand>
    {
        public CreatePaymentCommandValidator()
        {
            RuleFor(x => x.Data.OrderId)
                .NotEmpty()
                .WithMessage("Order ID is required.")
                .GreaterThan(0)
                .WithMessage("Order ID must be greater than 0.");

            RuleFor(x => x.Data.AmountPaid)
                .NotEmpty()
                .WithMessage("Amount paid is required.")
                .GreaterThan(0)
                .WithMessage("Amount paid must be greater than 0.");
        }
    }
}
