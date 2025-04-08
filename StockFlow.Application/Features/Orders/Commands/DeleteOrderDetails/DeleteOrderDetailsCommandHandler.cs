using MediatR;
using StockFlow.Api.Domain.Entities;
using StockFlow.Application.Common.Constants;
using StockFlow.Application.Interfaces;

namespace StockFlow.Application.Features.Orders.Commands.DeleteOrderDetails
{
    public class DeleteOrderDetailsCommandHandler : IRequestHandler<DeleteOrderDetailsCommand, bool>
    {
        private readonly IRepository<OrderDetailEntity> _repository;

        public DeleteOrderDetailsCommandHandler(IRepository<OrderDetailEntity> repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteOrderDetailsCommand request, CancellationToken cancellationToken)
        {
            var detail = await _repository.GetByIdAsync(request.Id);

            if(detail is null)
            {
                return false;
            }

            _repository.Remove(detail);

            return await _repository.SaveChangesAsync() > 0;
        }
    }
}
