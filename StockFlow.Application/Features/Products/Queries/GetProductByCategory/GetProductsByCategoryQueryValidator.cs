using FluentValidation;

namespace StockFlow.Application.Features.Products.Queries.GetProductByCategory
{
    public class GetProductsByCategoryQueryValidator : AbstractValidator<GetProductsByCategoryQuery>
    {
        public GetProductsByCategoryQueryValidator()
        {
            RuleFor(x => x.CategoryId)
                .NotEmpty()
                .WithMessage("Category ID is required.")
                .GreaterThan(0)
                .WithMessage("Category ID must be greater than 0.");
        }
    }
}
