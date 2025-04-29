using AutoMapper;
using MediatR;
using StockFlow.Api.Domain.Entities;
using StockFlow.Application.Cache;
using StockFlow.Application.Common.Constants;
using StockFlow.Application.Interfaces;

namespace StockFlow.Application.Features.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ICacheService cacheService) : IRequestHandler<CreateOrderCommand, Result<int>>
    {
        public async Task<Result<int>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var orderEntity = mapper.Map<OrderEntity>(request.Data);

            orderEntity.OrderDetails = [.. request.Data.OrderDetails.Select(detail =>
            {
                var orderDetailEntity = mapper.Map<OrderDetailEntity>(detail);
                orderDetailEntity.OrderId = orderEntity.Id;
                return orderDetailEntity;
            })];

            // Save Order and OrderDetails together
            await unitOfWork.Orders.AddAsync(orderEntity, cancellationToken);
            var saveResult = await unitOfWork.SaveChangesAsync(cancellationToken) > 0;

            if (!saveResult)
            {
                return Result<int>.Failure("An error occurred while creating the order.");
            }

            await cacheService.RemoveAsync(CacheKeys.OrdersByCustomerId(request.Data.CustomerId));

            return Result<int>.Success(orderEntity.Id);
        }
    }
}
