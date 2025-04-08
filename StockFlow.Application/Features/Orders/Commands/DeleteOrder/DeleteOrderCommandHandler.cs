using MediatR;
using StockFlow.Api.Domain.Entities;
using StockFlow.Application.Common.Constants;
using StockFlow.Application.Interfaces;

namespace StockFlow.Application.Features.Orders.Commands.DeleteOrder
{
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, bool>
    {
        private readonly IRepository<OrderEntity> _orderRepository;
        private readonly IRepository<OrderDetailEntity> _orderDetailRepository;
        private readonly ICacheService _cache;

        public DeleteOrderCommandHandler(IRepository<OrderEntity> orderRepository, IRepository<OrderDetailEntity> orderDetailRepository, ICacheService cache)
        {
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
            _cache = cache;
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

            await _cache.RemoveAsync(CacheKeys.OrderById(request.id));
            return await _orderRepository.SaveChangesAsync() > 0;
            
        }
    }
}
