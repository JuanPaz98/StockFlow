using AutoMapper;
using MediatR;
using StockFlow.Api.Domain.Entities;
using StockFlow.Application.Cache;
using StockFlow.Application.Common.Constants;
using StockFlow.Application.Features.Dtos.Suppliers;
using StockFlow.Application.Interfaces;

namespace StockFlow.Application.Features.Suppliers.Queries.GetSupplierById
{
    public class GetSupplierByIdQueryHandler(
        IUnitOfWork unitOfWork, 
        ICacheService cacheService, 
        IMapper mapper) : IRequestHandler<GetSupplierByIdQuery, Result<SupplierResponseDto>>
    {
        public async Task<Result<SupplierResponseDto>> Handle(GetSupplierByIdQuery request, CancellationToken cancellationToken)
        {
            var supplierKey = CacheKeys.SupplierById(request.Id);
            var supplierCached = await cacheService.GetAsync<SupplierResponseDto>(supplierKey);

            if (supplierCached is not null)
            {
                return Result<SupplierResponseDto>.Success(supplierCached);
            }

            var supplier = await unitOfWork.Suppliers.GetByIdAsync(request.Id, cancellationToken);

            var supplierModel = mapper.Map<SupplierResponseDto>(supplier);

            await cacheService.SetAsync(supplierKey, supplierModel);

            return Result<SupplierResponseDto>.Success(supplierModel);
        }
    }
}
