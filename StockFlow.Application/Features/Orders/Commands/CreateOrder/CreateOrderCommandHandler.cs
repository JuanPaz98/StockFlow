using AutoMapper;
using MediatR;
using StockFlow.Api.Domain.Entities;
using StockFlow.Application.Features.Orders.Dtos;
using StockFlow.Application.Interfaces;

namespace StockFlow.Application.Features.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, OrderResponseDto>
    {
        private readonly IRepository<OrderEntity> _orderRepository;
        private readonly IMapper _mapper;

        public CreateOrderCommandHandler(IRepository<OrderEntity> orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<OrderResponseDto> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
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

            return _mapper.Map<OrderResponseDto>(orderEntity);
        }

    }
}
