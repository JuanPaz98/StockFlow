using AutoMapper;
using MediatR;
using StockFlow.Api.Domain.Entities;
using StockFlow.Application.Common.Constants;
using StockFlow.Application.Interfaces;

namespace StockFlow.Application.Features.Orders.Queries.GetOrdersByCustomerId
{
    public class GetOrdersByCustomerIdQueryHandler : IRequestHandler<GetOrdersByCustomerIdQuery, IEnumerable<GetOrdersByCustomerIdModel>>
    {
        private readonly IRepository<OrderEntity> _orderRepository;
        private readonly IRepository<OrderDetailEntity> _orderDetailRepository;
        private readonly IMapper _mapper;
        private readonly ICacheService _cache;

        public GetOrdersByCustomerIdQueryHandler(
            IRepository<OrderEntity> orderRepository, 
            IRepository<OrderDetailEntity> orderDetailRepository, 
            IMapper mapper,
            ICacheService cache)
        {
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<IEnumerable<GetOrdersByCustomerIdModel>> Handle(GetOrdersByCustomerIdQuery request, CancellationToken cancellationToken)
        {
            var ordersKey = CacheKeys.OrdersByCustomerId(request.id);

            var cachedOrders = await _cache.GetAsync<IEnumerable<GetOrdersByCustomerIdModel>>(ordersKey);

            if (cachedOrders != null)
            {
                return cachedOrders;
            }

            var orders = await _orderRepository.FindAsync(order => order.CustomerId == request.id);
            var ordersList = orders.ToList();

            if (!ordersList.Any())
            {
                return new List<GetOrdersByCustomerIdModel>();
            }

            var orderDetails = await _orderDetailRepository.FindAsync(detail => ordersList.Select(o => o.Id).Contains(detail.OrderId));

            var orderDetailsGrouped = orderDetails.ToList().GroupBy(d => d.OrderId)
                .ToDictionary(g => g.Key, g => g.ToList());

            foreach (var order in ordersList)
            {
                order.OrderDetails = orderDetailsGrouped.TryGetValue(order.Id, out var details) ? details : new List<OrderDetailEntity>();
            }

            var ordersModels = _mapper.Map<List<GetOrdersByCustomerIdModel>>(orders);

            await _cache.SetAsync(ordersKey, ordersModels);

            return ordersModels;
        }
    }
}
