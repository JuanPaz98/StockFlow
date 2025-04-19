using AutoMapper;
using EllipticCurve;
using MediatR;
using StockFlow.Api.Domain.Entities;
using StockFlow.Application.Common.Constants;
using StockFlow.Application.Interfaces;

namespace StockFlow.Application.Features.Orders.Queries.GetOrderById
{
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, GetOrderByIdModel>
    {
        private readonly IRepository<OrderEntity> _orderRepository;
        private readonly IRepository<OrderDetailEntity> _orderDetailRepository;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;

        public GetOrderByIdQueryHandler(
            IRepository<OrderEntity> orderRepository, 
            IRepository<OrderDetailEntity> orderDetailRepository,
            IMapper mapper,
            ICacheService cache)
        {
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
            _mapper = mapper;
            _cacheService = cache;
        }

        public async Task<GetOrderByIdModel> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var orderKey = CacheKeys.OrderById(request.Id);

            var cachedOrder = await _cacheService.GetAsync<GetOrderByIdModel>(orderKey);

            if (cachedOrder != null)
            {
                return cachedOrder;
            }

            var order = await _orderRepository.GetByIdAsync(request.Id);
            if (order == null)
            {
                throw new Exception();
            }
            var orderDetails = await _orderDetailRepository.FindAsync(detail => detail.OrderId == order.Id);
            order.OrderDetails = orderDetails.ToList();

            var orderModel = _mapper.Map<GetOrderByIdModel>(order);

            await _cacheService.SetAsync(orderKey, orderModel);

            return orderModel;
        }
    }
}
