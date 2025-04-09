using AutoMapper;
using MediatR;
using StockFlow.Api.Domain.Entities;
using StockFlow.Application.Common.Constants;
using StockFlow.Application.Features.Orders.Dtos;
using StockFlow.Application.Interfaces;

namespace StockFlow.Application.Features.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, int>
    {
        private readonly IRepository<OrderEntity> _orderRepository;
        private readonly IMapper _mapper;
        private readonly ICacheService _cache;

        public CreateOrderCommandHandler(IRepository<OrderEntity> orderRepository, IMapper mapper, ICacheService cache)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var orderEntity = _mapper.Map<OrderEntity>(request.model);

            if (orderEntity == null)
            {
                throw new Exception("All fields are required");
            }


            orderEntity.OrderDetails = request.model.OrderDetails.Select(detail =>
            {
                var orderDetailEntity = _mapper.Map<OrderDetailEntity>(detail);
                orderDetailEntity.OrderId = orderEntity.Id;
                return orderDetailEntity;
            }).ToList();

            // Save Order and OrderDetails together
            await _orderRepository.AddAsync(orderEntity);
            await _orderRepository.SaveChangesAsync(); 

            var orderModel = _mapper.Map<OrderWithIdDto>(orderEntity);

            await _cache.RemoveAsync(CacheKeys.OrdersByCustomerId(request.model.CustomerId));

            return orderEntity.Id;
        }

    }
}
