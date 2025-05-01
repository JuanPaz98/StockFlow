using FluentValidation;

namespace StockFlow.Application.Features.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryCommandValidator : AbstractValidator<DeleteCategoryCommand>
    {
        public DeleteCategoryCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Category ID is required.")
                .GreaterThan(0)
                .WithMessage("Category ID must be greater than 0.");
        }
    }
}
