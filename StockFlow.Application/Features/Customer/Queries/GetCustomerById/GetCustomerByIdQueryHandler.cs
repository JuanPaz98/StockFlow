using AutoMapper;
using MediatR;
using StockFlow.Api.Domain.Entities;
using StockFlow.Application.Common.Constants;
using StockFlow.Application.Interfaces;

namespace StockFlow.Application.Features.Customer.Queries.GetCustomerById
{
    public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, GetCustomerByIdModel>
    {
        private readonly IRepository<CustomerEntity> _repository;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;

        public GetCustomerByIdQueryHandler(IRepository<CustomerEntity> repository, IMapper mapper, ICacheService cache)
        {
            _repository = repository;
            _mapper = mapper;
            _cacheService = cache;
        }

        public async Task<GetCustomerByIdModel> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            var cacheKey = CacheKeys.CustomerById(request.id);

            var customerCached = await _cacheService.GetAsync<GetCustomerByIdModel>(cacheKey);

            if (customerCached != null)
            {
                return customerCached;
            }

            var customer = await _repository.GetByIdAsync(request.id);
            
            if (customer != null)
            {
                var customerModel = _mapper.Map<GetCustomerByIdModel>(customer);
                await _cacheService.SetAsync(cacheKey, customerModel);
                return customerModel;
            }
            return new GetCustomerByIdModel();
        }
    }
}

