using MediatR;
using StockFlow.Api.Domain.Entities;
using StockFlow.Application.Interfaces;

namespace StockFlow.Application.Features.Orders.Commands.DeleteOrder
{
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, bool>
    {
        private readonly IRepository<OrderEntity> _orderRepository;
        private readonly IRepository<OrderDetailEntity> _orderDetailRepository;

        public DeleteOrderCommandHandler(IRepository<OrderEntity> orderRepository, IRepository<OrderDetailEntity> orderDetailRepository)
        {
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
        }

        public async Task<bool> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.id);
            if (order == null)
            {
                return false;
            }

            var orderDetails = _orderDetailRepository.FindAsync(od => od.OrderId == order.Id).Result;

            if (orderDetails.Any())
            {
                _orderDetailRepository.RemoveRange(orderDetails);
            }

            _orderRepository.Remove(order);
            return await _orderRepository.SaveChangesAsync() > 0;
            
        }
    }
}
