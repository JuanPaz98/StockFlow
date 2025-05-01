using FluentValidation;

namespace StockFlow.Application.Features.Suppliers.Commands.UpdateSupplier
{
    public class UpdateSupplierCommandValidator : AbstractValidator<UpdateSupplierCommad>
    {
        public UpdateSupplierCommandValidator()
        {
            RuleFor(x => x.Data.Name)
                .NotEmpty()
                .WithMessage("Name is required.")
                .MaximumLength(100)
                .WithMessage("Name must not exceed 100 characters.");
        }
    }
}
