using FluentValidation;

namespace StockFlow.Application.Features.Suppliers.Queries.GetSupplierById
{
    public class GetSupplierByIdQueryValidator : AbstractValidator<GetSupplierByIdQuery>
    {
        public GetSupplierByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Supplier ID is required.")
                .GreaterThan(0)
                .WithMessage("Supplier ID must be greater than 0.");
        }
    }
}
