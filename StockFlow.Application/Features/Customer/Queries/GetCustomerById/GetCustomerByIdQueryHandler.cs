using AutoMapper;
using MediatR;
using StockFlow.Api.Domain.Entities;
using StockFlow.Application.Cache;
using StockFlow.Application.Common.Constants;
using StockFlow.Application.Features.Dtos.Customers;
using StockFlow.Application.Interfaces;
using StockFlow.Domain.Repositories;

namespace StockFlow.Application.Features.Customer.Queries.GetCustomerById
{
    public class GetCustomerByIdQueryHandler(
        IUnitOfWork unitOfWork, 
        IMapper mapper, 
        ICacheService cache) : IRequestHandler<GetCustomerByIdQuery, Result<CustomerResponseDto>>
    {
        public async Task<Result<CustomerResponseDto>> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            var cacheKey = CacheKeys.CustomerById(request.Id);

            var customerCached = await cache.GetAsync<CustomerResponseDto>(cacheKey);

            if (customerCached != null)
            {
                return Result<CustomerResponseDto>.Success(customerCached);
            }

            var customer = await unitOfWork.Customers.GetByIdAsync(request.Id, cancellationToken);
            
            if(customer == null)
            {
                return Result<CustomerResponseDto>.Failure($"Customer with ID {request.Id} not found.");
            }

            var customerModel = mapper.Map<CustomerResponseDto>(customer);
            await cache.SetAsync(cacheKey, customerModel);
            
            return Result<CustomerResponseDto>.Success(customerModel);
        }
    }
}

