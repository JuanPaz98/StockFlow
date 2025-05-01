using AutoMapper;
using MediatR;
using StockFlow.Application.Cache;
using StockFlow.Application.Common.Constants;
using StockFlow.Application.Features.Dtos.Customers;
using StockFlow.Application.Interfaces;

namespace StockFlow.Application.Features.Customer.Commands.UpdateCustomer
{
    public class UpdateCustomerCommandHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ICacheService cache) : IRequestHandler<UpdateCustomerCommand, Result<CustomerRequestIdDto>>
    {
        public async Task<Result<CustomerRequestIdDto>> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await unitOfWork.Customers.GetByIdAsync(request.Data.Id, cancellationToken);

            if (customer is null)
            {
                return Result<CustomerRequestIdDto>.Failure($"Customer with ID {request.Data.Id} not found.");
            }

            mapper.Map(request.Data, customer);

            unitOfWork.Customers.Update(customer);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            await cache.RemoveAsync(CacheKeys.AllCustomers);
            await cache.RemoveAsync(CacheKeys.CustomerById(request.Data.Id));

            return Result<CustomerRequestIdDto>.Success(mapper.Map<CustomerRequestIdDto>(customer));
        }
    }
}
