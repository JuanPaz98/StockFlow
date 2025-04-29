using FluentValidation;

namespace StockFlow.Application.Features.Categories.Queries.GetCategoryById
{
    public class GetCategoryByIdQueryValidator : AbstractValidator<GetCategoryByIdQuery>
    {
        public GetCategoryByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Category ID is required.")
                .GreaterThan(0)
                .WithMessage("Category ID must be greater than 0.");
        }
    }
}
