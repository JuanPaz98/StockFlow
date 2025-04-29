using AutoMapper;
using MediatR;
using StockFlow.Api.Domain.Entities;
using StockFlow.Application.Cache;
using StockFlow.Application.Common.Constants;
using StockFlow.Application.Interfaces;
using StockFlow.Domain.Repositories;

namespace StockFlow.Application.Features.Customer.Commands.CreateCustomer
{
    public class CreateCustomerCommandHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ICacheService cacheService) : IRequestHandler<CreateCustomerCommand, Result<int>>
    {
        public async Task<Result<int>> Handle(CreateCustomerCommand request,
            CancellationToken cancellationToken)
        {
            var customerEntity = mapper.Map<CustomerEntity>(request.Data);

            await unitOfWork.Customers.AddAsync(customerEntity, cancellationToken);
            var saveResult = await unitOfWork.SaveChangesAsync(cancellationToken) > 0;

            if (!saveResult)
            {
                return Result<int>.Failure("An error occurred while creating the customer.");
            }

            await cacheService.RemoveAsync(CacheKeys.AllCustomers);

            return Result<int>.Success(customerEntity.Id);
        }
    }
}
