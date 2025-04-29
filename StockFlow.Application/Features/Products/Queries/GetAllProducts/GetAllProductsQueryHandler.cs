using AutoMapper;
using MediatR;
using StockFlow.Application.Cache;
using StockFlow.Application.Common.Constants;
using StockFlow.Application.Features.Dtos.Products;
using StockFlow.Application.Interfaces;

namespace StockFlow.Application.Features.Products.Queries.GetAllProducts
{
    public class GetAllProductsQueryHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ICacheService cacheService) : IRequestHandler<GetAllProductsQuery, Result<IEnumerable<ProductResponseDto>>>
    {
        public async Task<Result<IEnumerable<ProductResponseDto>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var productsKey = CacheKeys.AllProducts;

            var cachedProducts = await cacheService.GetAsync<IEnumerable<ProductResponseDto>>(productsKey);

            if (cachedProducts != null)
            {
                return Result<IEnumerable<ProductResponseDto>>.Success(cachedProducts);
            }

            var products = await unitOfWork.Products.GetAllAsync(cancellationToken);
            var productsModels = mapper.Map<IEnumerable<ProductResponseDto>>(products);

            await cacheService.SetAsync(CacheKeys.AllProducts, productsModels);

            return Result<IEnumerable<ProductResponseDto>>.Success(productsModels);
        }
    }
}
