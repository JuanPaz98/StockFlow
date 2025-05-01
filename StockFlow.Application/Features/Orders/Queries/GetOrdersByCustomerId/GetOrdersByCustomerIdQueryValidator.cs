using FluentValidation;

namespace StockFlow.Application.Features.Orders.Queries.GetOrdersByCustomerId
{
    public class GetOrdersByCustomerIdQueryValidator : AbstractValidator<GetOrdersByCustomerIdQuery>
    {
        public GetOrdersByCustomerIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Customer ID is required.")
                .GreaterThan(0)
                .WithMessage("Customer ID must be greater than 0.");
        }
    }
}
