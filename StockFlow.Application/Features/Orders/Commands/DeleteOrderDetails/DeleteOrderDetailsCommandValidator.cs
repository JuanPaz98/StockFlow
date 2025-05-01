using FluentValidation;

namespace StockFlow.Application.Features.Orders.Commands.DeleteOrderDetails
{
    class DeleteOrderDetailsCommandValidator : AbstractValidator<DeleteOrderDetailsCommand>
    {
        public DeleteOrderDetailsCommandValidator()
        {
            RuleFor(x => x.Id)
               .NotEmpty()
               .WithMessage("Detail ID is required.")
               .GreaterThan(0)
               .WithMessage("Detail ID must be greater than 0.");
        }
    }
}
