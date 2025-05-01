using FluentValidation;

namespace StockFlow.Application.Features.Orders.Queries.GetOrderById
{
    public class GetOrderByIdQueryValidator : AbstractValidator<GetOrderByIdQuery>
    {
        public GetOrderByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Order ID is required.")
                .GreaterThan(0)
                .WithMessage("Order ID must be greater than 0.");
        }
    }
}
