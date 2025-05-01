using FluentValidation;

namespace StockFlow.Application.Features.Orders.Commands.DeleteOrder
{
    public class DeleteOrderCommandValidator : AbstractValidator<DeleteOrderCommand>
    {
        public DeleteOrderCommandValidator() {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Order ID is required.")
                .GreaterThan(0)
                .WithMessage("Order ID must be greater than 0.");
        }
    }
}
