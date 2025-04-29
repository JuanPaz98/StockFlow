using AutoMapper;
using MediatR;
using StockFlow.Api.Domain.Entities;
using StockFlow.Application.Cache;
using StockFlow.Application.Common.Constants;
using StockFlow.Application.Features.Dtos.Suppliers;
using StockFlow.Application.Interfaces;

namespace StockFlow.Application.Features.Suppliers.Queries.GetAllSuppliers
{
    public class GetAllSuppliersQueryHandler(
        IUnitOfWork unitOfWork, 
        ICacheService cacheService, 
        IMapper mapper) : IRequestHandler<GetAllSuppliersQuery, Result<IEnumerable<SupplierResponseDto>>>
    {
        public async Task<Result<IEnumerable<SupplierResponseDto>>> Handle(GetAllSuppliersQuery request, CancellationToken cancellationToken)
        {
            var suppliersKey = CacheKeys.AllSuppliers;

            var cachedSuppliers = await cacheService.GetAsync<IEnumerable<SupplierResponseDto>>(suppliersKey);

            if (cachedSuppliers != null)
            {
                return Result<IEnumerable<SupplierResponseDto>>.Success(cachedSuppliers);
            }

            var suppliers = await unitOfWork.Suppliers.GetAllAsync(cancellationToken);

            if (!suppliers.Any())
            {
                return Result<IEnumerable<SupplierResponseDto>>.Success(Enumerable.Empty<SupplierResponseDto>());
            }

            var suppliersModels = mapper.Map<IEnumerable<SupplierResponseDto>>(suppliers);

            await cacheService.SetAsync(suppliersKey, suppliersModels);

            return Result<IEnumerable<SupplierResponseDto>>.Success(suppliersModels);
        }
    }
}
