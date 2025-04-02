using AutoMapper;
using EllipticCurve;
using MediatR;
using StockFlow.Api.Domain.Entities;
using StockFlow.Application.Interfaces;

namespace StockFlow.Application.Features.Orders.Queries.GetOrderById
{
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, GetOrderByIdModel>
    {
        private readonly IRepository<OrderEntity> _orderRepository;
        private readonly IRepository<OrderDetailEntity> _orderDetailRepository;
        private readonly IMapper _mapper;

        public GetOrderByIdQueryHandler(
            IRepository<OrderEntity> orderRepository, 
            IRepository<OrderDetailEntity> orderDetailRepository,
            IMapper mapper)
        {
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
            _mapper = mapper;
        }

        public async Task<GetOrderByIdModel> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.Id);
            if (order == null)
            {
                throw new Exception();
            }
            var orderDetails = await _orderDetailRepository.FindAsync(detail => detail.OrderId == order.Id);
            order.OrderDetails = orderDetails.ToList();

            return _mapper.Map<GetOrderByIdModel>(order);
        }
    }
}
