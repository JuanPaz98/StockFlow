using AutoMapper;
using MediatR;
using StockFlow.Application.Cache;
using StockFlow.Application.Common.Constants;
using StockFlow.Application.Features.Dtos.Customers;
using StockFlow.Application.Interfaces;

namespace StockFlow.Application.Features.Customer.Queries.GetAllCustomers
{
    public class GetAllCustomersQueryHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ICacheService cacheService) : IRequestHandler<GetAllCustomersQuery, Result<IEnumerable<CustomerResponseDto>>>
    {
        public async Task<Result<IEnumerable<CustomerResponseDto>>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
        {
            string cacheKey = CacheKeys.AllCustomers;

            var cachedCustomers = await cacheService.GetAsync<IEnumerable<CustomerResponseDto>>(cacheKey);
            if (cachedCustomers != null && cachedCustomers.Any())
            {
                return Result<IEnumerable<CustomerResponseDto>>.Success(cachedCustomers);
            }

            var customers = await unitOfWork.Customers.GetAllAsync(cancellationToken);

            var customerModels = mapper.Map<IEnumerable<CustomerResponseDto>>(customers);

            await cacheService.SetAsync(cacheKey, customerModels);

            return Result<IEnumerable<CustomerResponseDto>>.Success(customerModels);
        }
    }
}
