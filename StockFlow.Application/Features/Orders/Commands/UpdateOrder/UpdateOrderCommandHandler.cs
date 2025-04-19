using AutoMapper;
using MediatR;
using SendGrid.Helpers.Errors.Model;
using StockFlow.Api.Domain.Entities;
using StockFlow.Application.Common.Constants;
using StockFlow.Application.Features.Dtos.Orders;
using StockFlow.Application.Interfaces;

namespace StockFlow.Application.Features.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, OrderWithIdDto>
    {
        private readonly IRepository<OrderEntity> _orderRepository;
        private readonly IRepository<OrderDetailEntity> _orderDetailRepository;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;

        public UpdateOrderCommandHandler(
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

        public async Task<OrderWithIdDto> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.model.Id);

            if (order is null)
            {
                throw new NotFoundException($"Order with ID {request.model.Id} not found.");
            }

            _mapper.Map(request.model, order);

            UpdateOrderDetails(order, request.model);

            await _orderRepository.SaveChangesAsync();

            var orderModel = _mapper.Map<OrderWithIdDto>(order);

            await _cacheService.RemoveAsync(CacheKeys.OrdersByCustomerId(request.model.CustomerId));
            await _cacheService.RemoveAsync(CacheKeys.OrderById(request.model.Id));

            return orderModel;
        }

        private void UpdateOrderDetails(OrderEntity order, OrderWithIdDto model)
        {
            var existingDetails = order.OrderDetails.ToList();
            var updatedDetails = model.OrderDetails.ToList();

            foreach (var detail in existingDetails)
            {
                if (!updatedDetails.Any(ud => ud.Id == detail.Id && detail.Id != 0))
                {
                    order.OrderDetails.Remove(detail);
                }
            }

            foreach (var detailModel in updatedDetails)
            {
                var existingDetail = existingDetails.FirstOrDefault(od => od.Id == detailModel.Id && detailModel.Id != 0);

                if (existingDetail != null)
                {
                    _mapper.Map(detailModel, existingDetail);
                }
                else
                {
                    var newDetail = _mapper.Map<OrderDetailEntity>(detailModel);
                    newDetail.OrderId = order.Id;
                    order.OrderDetails.Add(newDetail);
                }
            }
        }
    }
}
