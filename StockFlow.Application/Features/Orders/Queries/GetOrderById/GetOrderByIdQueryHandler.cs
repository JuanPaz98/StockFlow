using AutoMapper;
using MediatR;
using StockFlow.Application.Cache;
using StockFlow.Application.Common.Constants;
using StockFlow.Application.Features.Dtos.Orders;
using StockFlow.Application.Interfaces;

namespace StockFlow.Application.Features.Orders.Queries.GetOrderById
{
    public class GetOrderByIdQueryHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ICacheService cache) : IRequestHandler<GetOrderByIdQuery, Result<OrderWithIdDto>>
    {
        public async Task<Result<OrderWithIdDto>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var orderKey = CacheKeys.OrderById(request.Id);

            var cachedOrder = await cache.GetAsync<OrderWithIdDto>(orderKey);

            if (cachedOrder != null)
            {
                return Result<OrderWithIdDto>.Success(cachedOrder);
            }

            var order = await unitOfWork.Orders.GetByIdAsync(request.Id, cancellationToken);
            if (order == null)
            {
                return Result<OrderWithIdDto>.Failure($"Order with ID {request.Id} not found.");
            }
            var orderDetails = await unitOfWork.OrderDetails.FindAsync(detail => detail.OrderId == order.Id, cancellationToken);
            order.OrderDetails = [.. orderDetails];

            var orderModel = mapper.Map<OrderWithIdDto>(order);

            await cache.SetAsync(orderKey, orderModel);

            return Result<OrderWithIdDto>.Success(orderModel);
        }
    }
}
