using AutoMapper;
using MediatR;
using StockFlow.Api.Domain.Entities;
using StockFlow.Application.Interfaces;

namespace StockFlow.Application.Features.Orders.Queries.GetOrdersByCustomerId
{
    public class GetOrdersByCustomerIdQueryHandler : IRequestHandler<GetOrdersByCustomerIdQuery, List<GetOrdersByCustomerIdModel>>
    {
        private readonly IRepository<OrderEntity> _repository;
        private readonly IMapper _mapper;

        public GetOrdersByCustomerIdQueryHandler(IRepository<OrderEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetOrdersByCustomerIdModel>> Handle(GetOrdersByCustomerIdQuery request, CancellationToken cancellationToken)
        {
            var orders = await _repository.FindAsync(order => order.CustomerId == request.id);

            return _mapper.Map<List<GetOrdersByCustomerIdModel>>(orders);
        }
    }
}
