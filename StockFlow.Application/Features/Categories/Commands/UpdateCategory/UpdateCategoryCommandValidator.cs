using FluentValidation;

namespace StockFlow.Application.Features.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommandValidator: AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryCommandValidator() {
            RuleFor(x => x.Data.Id)
               .NotNull()
               .WithMessage("Category ID cannot be null.")
               .NotEmpty()
               .WithMessage("Category ID cannot be empty.")
               .GreaterThan(0)
               .WithMessage("Category ID must be greater than 0.");

            RuleFor(x => x.Data.Name)
                .NotNull()
                .WithMessage("Category name cannot be null.")
                .NotEmpty()
                .WithMessage("Category name cannot be empty.")
                .MaximumLength(100)
                .WithMessage("Category name must not exceed 100 characters.");
        }
    }
}
