using AutoMapper;
using MediatR;
using StockFlow.Api.Domain.Entities;
using StockFlow.Application.Cache;
using StockFlow.Application.Common.Constants;
using StockFlow.Application.Features.Dtos.Orders;
using StockFlow.Application.Interfaces;

namespace StockFlow.Application.Features.Orders.Queries.GetOrdersByCustomerId
{
    public class GetOrdersByCustomerIdQueryHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ICacheService cacheService) : IRequestHandler<GetOrdersByCustomerIdQuery, Result<IEnumerable<OrderWithIdDto>>>
    {
        public async Task<Result<IEnumerable<OrderWithIdDto>>> Handle(GetOrdersByCustomerIdQuery request, CancellationToken cancellationToken)
        {
            var ordersKey = CacheKeys.OrdersByCustomerId(request.Id);

            var cachedOrders = await cacheService.GetAsync<IEnumerable<OrderWithIdDto>>(ordersKey);

            if (cachedOrders != null)
            {
                return Result<IEnumerable<OrderWithIdDto>>.Success(cachedOrders);
            }

            var orders = await unitOfWork.Orders.FindAsync(order => order.CustomerId == request.Id, cancellationToken);
            var ordersList = orders.ToList();

            if (ordersList.Count == 0)
            {
                return Result<IEnumerable<OrderWithIdDto>>.Success(Enumerable.Empty<OrderWithIdDto>());
            }

            var orderDetails = await unitOfWork.OrderDetails
                .FindAsync(detail => ordersList.Select(o => o.Id).Contains(detail.OrderId), cancellationToken);

            var orderDetailsGrouped = orderDetails.ToList().GroupBy(d => d.OrderId)
                .ToDictionary(g => g.Key, g => g.ToList());

            foreach (var order in ordersList)
            {
                order.OrderDetails = orderDetailsGrouped.TryGetValue(order.Id, out var details) ? details : new List<OrderDetailEntity>();
            }

            var ordersModels = mapper.Map<List<OrderWithIdDto>>(orders);

            await cacheService.SetAsync(ordersKey, ordersModels);

            return Result<IEnumerable<OrderWithIdDto>>.Success(ordersModels);
        }
    }
}
