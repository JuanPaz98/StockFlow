using FluentValidation;

namespace StockFlow.Application.Features.Payments.Queries.GetPaymentsByOrderId
{
    public class GetPaymentsByOrderIdQueryValidator : AbstractValidator<GetPaymentsByOrderIdQuery>
    {
        public GetPaymentsByOrderIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Order ID is required.")
                .GreaterThan(0)
                .WithMessage("Order ID must be greater than 0.");
        }
    }
}
