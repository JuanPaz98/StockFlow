using FluentValidation;

namespace StockFlow.Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.Data.Id)
                .NotNull()
                .WithMessage("Product ID is required.")
                .GreaterThan(0)
                .WithMessage("Product ID must be greater than 0.");

            RuleFor(x => x.Data.Name)
                 .NotNull()
                 .WithMessage("Name is required.")
                 .MinimumLength(3)
                 .WithMessage("Name must be at least 3 characters long.");

            RuleFor(x => x.Data.Price)
                .NotNull()
                .WithMessage("Price is required.")
                .GreaterThan(0)
                .WithMessage("Price must be greater than 0.");

            RuleFor(x => x.Data.Stock)
                .NotNull()
                .WithMessage("Stock is required.")
                .GreaterThanOrEqualTo(0)
                .WithMessage("Stock must be greater than or equal to 0.");

            RuleFor(x => x.Data.CategoryId)
                .NotNull()
                .WithMessage("Category ID is required.")
                .GreaterThan(0)
                .WithMessage("Category ID must be greater than 0.");

        }
    }
}
