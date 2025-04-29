using MediatR;
using StockFlow.Api.Domain.Entities;
using StockFlow.Application.Cache;
using StockFlow.Application.Common.Constants;
using StockFlow.Application.Interfaces;
using StockFlow.Domain.Repositories;

namespace StockFlow.Application.Features.Orders.Commands.DeleteOrder
{
    public class DeleteOrderCommandHandler(
        IUnitOfWork unitOfWork,
        ICacheService cacheService) : IRequestHandler<DeleteOrderCommand, Result<bool>>
    {
        public async Task<Result<bool>> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await unitOfWork.Orders.GetByIdAsync(request.Id, cancellationToken);
            if (order == null)
            {
                return Result<bool>.Failure($"Order with ID {request.Id} not found.");
            }

            var orderDetails = unitOfWork.OrderDetails.FindAsync(od => od.OrderId == order.Id, cancellationToken).Result;

            if (orderDetails.Any())
            {
                unitOfWork.OrderDetails.RemoveRange(orderDetails);
            }

            unitOfWork.Orders.Remove(order);

            var saveResult = await unitOfWork.SaveChangesAsync(cancellationToken) > 0;

            if (!saveResult)
            {
                return Result<bool>.Failure("An error occurred while deleting the order.");
            }

            await cacheService.RemoveAsync(CacheKeys.OrderById(request.Id));
            return Result<bool>.Success(saveResult);
            
        }
    }
}
