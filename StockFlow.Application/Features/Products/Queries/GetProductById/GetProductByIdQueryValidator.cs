using FluentValidation;

namespace StockFlow.Application.Features.Products.Queries.GetProductById
{
    public class GetProductByIdQueryValidator : AbstractValidator<GetProductByIdQuery>
    {
        public GetProductByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Product ID is required.")
                .GreaterThan(0)
                .WithMessage("Product ID must be greater than 0.");
        }
    }
}
