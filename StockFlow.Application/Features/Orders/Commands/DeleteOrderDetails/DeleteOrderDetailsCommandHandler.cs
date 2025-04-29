using MediatR;
using StockFlow.Application.Interfaces;

namespace StockFlow.Application.Features.Orders.Commands.DeleteOrderDetails
{
    public class DeleteOrderDetailsCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteOrderDetailsCommand, Result<bool>>
    {
        public async Task<Result<bool>> Handle(DeleteOrderDetailsCommand request, CancellationToken cancellationToken)
        {
            var detail = await unitOfWork.OrderDetails.GetByIdAsync(request.Id, cancellationToken);

            if (detail is null)
            {
                return Result<bool>.Failure($"Order detail with ID {request.Id} not found.");
            }

            unitOfWork.OrderDetails.Remove(detail);

            var saveResult = await unitOfWork.SaveChangesAsync(cancellationToken) > 0;

            return Result<bool>.Success(saveResult);
        }
    }
}
