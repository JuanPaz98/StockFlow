using AutoMapper;
using EllipticCurve.Utils;
using MediatR;
using SendGrid.Helpers.Errors.Model;
using StockFlow.Api.Domain.Entities;
using StockFlow.Application.Features.Orders.Commands.CreateOrder;
using StockFlow.Application.Interfaces;

namespace StockFlow.Application.Features.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, UpdateOrderModel>
    {
        private readonly IRepository<OrderEntity> _orderRepository;
        private readonly IRepository<OrderDetailEntity> _orderDetailRepository;
        private readonly IMapper _mapper;

        public UpdateOrderCommandHandler(
            IRepository<OrderEntity> orderRepository,
            IRepository<OrderDetailEntity> orderDetailRepository,
            IMapper mapper)
        {
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
            _mapper = mapper;
        }

        public async Task<UpdateOrderModel> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.model.Id);

            if (order is null)
            {
                throw new NotFoundException($"Order with ID {request.model.Id} not found.");
            }

            _mapper.Map(request.model, order);

            UpdateOrderDetails(order, request.model);

            await _orderRepository.SaveChangesAsync();

            return _mapper.Map<UpdateOrderModel>(order);
        }

        private void UpdateOrderDetails(OrderEntity order, UpdateOrderModel model)
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
