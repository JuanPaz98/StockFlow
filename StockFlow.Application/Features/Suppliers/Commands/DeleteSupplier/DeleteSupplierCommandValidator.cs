using FluentValidation;

namespace StockFlow.Application.Features.Suppliers.Commands.DeleteSupplier
{
    public class DeleteSupplierCommandValidator : AbstractValidator<DeleteSupplierCommand>
    {
        public DeleteSupplierCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Supplier ID is required.");
        }
    }
}
