using AutoMapper;
using MediatR;
using StockFlow.Api.Domain.Entities;
using StockFlow.Application.Cache;
using StockFlow.Application.Common.Constants;
using StockFlow.Application.Interfaces;

namespace StockFlow.Application.Features.Suppliers.Commands.CreateSupplier
{
    public class CreateSupplierCommandHandler(
        IUnitOfWork unitOfWork,
        ICacheService cacheService,
        IMapper mapper) : IRequestHandler<CreateSupplierCommand, Result<int>>
    {
        public async Task<Result<int>> Handle(CreateSupplierCommand request, CancellationToken cancellationToken)
        {
            var supplierEntity = mapper.Map<SupplierEntity>(request.Data);

            await unitOfWork.Suppliers.AddAsync(supplierEntity, cancellationToken);

            await cacheService.RemoveAsync(CacheKeys.AllSuppliers);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<int>.Success(supplierEntity.Id);
        }
    }
}
