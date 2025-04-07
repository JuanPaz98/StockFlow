using AutoMapper;
using MediatR;
using StockFlow.Api.Domain.Entities;
using StockFlow.Application.Interfaces;

namespace StockFlow.Application.Features.Customer.Queries.GetAllCustomers
{
    public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, List<GetAllCustomersModel>>
    {
        private readonly IRepository<CustomerEntity> _repository;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;

        public GetAllCustomersQueryHandler(IRepository<CustomerEntity> repository,  IMapper mapper, ICacheService cacheService)
        {
            _repository = repository;
            _mapper = mapper;
            _cacheService = cacheService;
        }

        public async Task<List<GetAllCustomersModel>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
        {
            string cacheKey = "customers: all";

            var cachedCustomers = await _cacheService.GetAsync<List<GetAllCustomersModel>>(cacheKey);
            if (cachedCustomers != null)
            {
                return cachedCustomers;
            }

            var customers = await _repository.GetAllAsync();

            var customerModels = _mapper.Map<List<GetAllCustomersModel>>(customers);

            await _cacheService.SetAsync(cacheKey, customerModels);

            return customerModels;
        }
    }
}
