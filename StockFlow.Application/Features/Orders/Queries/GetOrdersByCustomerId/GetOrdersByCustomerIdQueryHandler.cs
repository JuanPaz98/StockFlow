using AutoMapper;
using MediatR;
using StockFlow.Api.Domain.Entities;
using StockFlow.Application.Interfaces;

namespace StockFlow.Application.Features.Orders.Queries.GetOrdersByCustomerId
{
    public class GetOrdersByCustomerIdQueryHandler : IRequestHandler<GetOrdersByCustomerIdQuery, List<GetOrdersByCustomerIdModel>>
    {
        private readonly IRepository<OrderEntity> _orderRepository;
        private readonly IRepository<OrderDetailEntity> _orderDetailRepository;
        private readonly IMapper _mapper;

        public GetOrdersByCustomerIdQueryHandler(IRepository<OrderEntity> orderRepository, IRepository<OrderDetailEntity> orderDetailRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
            _mapper = mapper;
        }

        public async Task<List<GetOrdersByCustomerIdModel>> Handle(GetOrdersByCustomerIdQuery request, CancellationToken cancellationToken)
        {
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

            return _mapper.Map<List<GetOrdersByCustomerIdModel>>(orders);
        }
    }
}
