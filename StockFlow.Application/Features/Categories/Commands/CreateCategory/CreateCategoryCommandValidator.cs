using FluentValidation;
using StockFlow.Application.Features.Dtos.Categories;

namespace StockFlow.Application.Features.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(x => x.Data.Name)
                .NotEmpty()
                .WithMessage("Category name is required.")
                .MaximumLength(100)
                .WithMessage("Category name must not exceed 100 characters.");
        }
    }
}
