using FluentValidation;

namespace StockFlow.Application.Features.Customer.Queries.GetCustomerById
{
    public class GetCustomerByIdQueryValidator : AbstractValidator<GetCustomerByIdQuery>
    {
        public GetCustomerByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Customer ID is required.")
                .GreaterThan(0)
                .WithMessage("Customer ID must be greater than 0.");
        }
    }
}
