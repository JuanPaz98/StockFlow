using AutoMapper;
using MediatR;
using SendGrid.Helpers.Errors.Model;
using StockFlow.Api.Domain.Entities;
using StockFlow.Application.Cache;
using StockFlow.Application.Common.Constants;
using StockFlow.Application.Features.Dtos.Orders;
using StockFlow.Application.Interfaces;
using StockFlow.Domain.Repositories;

namespace StockFlow.Application.Features.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommandHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ICacheService cacheService) : IRequestHandler<UpdateOrderCommand, Result<OrderWithIdDto>>
    {
        public async Task<Result<OrderWithIdDto>> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await unitOfWork.Orders.GetByIdAsync(request.Data.Id, cancellationToken);

            if (order is null)
            {
                return Result<OrderWithIdDto>.Failure($"Order with ID {request.Data.Id} not found.");
            }

            mapper.Map(request.Data, order);

            UpdateOrderDetails(order, request.Data);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            var orderModel = mapper.Map<OrderWithIdDto>(order);

            await cacheService.RemoveAsync(CacheKeys.OrdersByCustomerId(request.Data.CustomerId));
            await cacheService.RemoveAsync(CacheKeys.OrderById(request.Data.Id));

            return Result<OrderWithIdDto>.Success(orderModel);
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
                    mapper.Map(detailModel, existingDetail);
                }
                else
                {
                    var newDetail = mapper.Map<OrderDetailEntity>(detailModel);
                    newDetail.OrderId = order.Id;
                    order.OrderDetails.Add(newDetail);
                }
            }
        }
    }
}
