using AutoMapper;
using MediatR;
using StockFlow.Api.Domain.Entities;
using StockFlow.Application.Interfaces;

namespace StockFlow.Application.Features.Customer.Queries.GetCustomerById
{
    public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, GetCustomerByIdModel>
    {
        private readonly IRepository<CustomerEntity> _repository;
        private readonly IMapper _mapper;

        public GetCustomerByIdQueryHandler(IRepository<CustomerEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GetCustomerByIdModel> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            var customer = await _repository.GetByIdAsync(request.id);         
            
            if (customer != null)
            {
                return _mapper.Map<GetCustomerByIdModel>(customer);
            }
            return new GetCustomerByIdModel();
        }
    }
}

