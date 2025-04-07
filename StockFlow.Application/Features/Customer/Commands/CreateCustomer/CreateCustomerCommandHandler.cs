using AutoMapper;
using MediatR;
using StockFlow.Api.Domain.Entities;
using StockFlow.Application.Interfaces;

namespace StockFlow.Application.Features.Customer.Commands.CreateCustomer
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, int>
    {
        private readonly IRepository<CustomerEntity> _repository;
        private readonly IMapper _mapper;
        private readonly ICacheService _cache;
        public CreateCustomerCommandHandler(IRepository<CustomerEntity> repository, IMapper mapper, ICacheService cache)
        {
            _repository = repository;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<int> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {

            var customerEntity = _mapper.Map<CustomerEntity>(request);

            if (string.IsNullOrEmpty(customerEntity.Name))
            {
                throw new Exception("Name is required");
            }

            await _repository.AddAsync(customerEntity);

            return await _repository.SaveChangesAsync();
        }
    }
}
