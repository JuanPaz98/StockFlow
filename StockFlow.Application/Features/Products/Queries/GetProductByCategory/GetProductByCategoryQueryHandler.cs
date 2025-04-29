using AutoMapper;
using MediatR;
using StockFlow.Application.Cache;
using StockFlow.Application.Common.Constants;
using StockFlow.Application.Features.Dtos.Products;
using StockFlow.Application.Interfaces;

namespace StockFlow.Application.Features.Products.Queries.GetProductByCategory
{
    public class GetProductByCategoryQueryHandler(
        IUnitOfWork unitOfWork,
        ICacheService cacheService,
        IMapper mapper
        ) : IRequestHandler<GetProductsByCategoryQuery, Result<IEnumerable<ProductResponseDto>>>
    {
        public async Task<Result<IEnumerable<ProductResponseDto>>> Handle(GetProductsByCategoryQuery request, CancellationToken cancellationToken)
        {
            string categoryKey = CacheKeys.CategoryById(request.CategoryId);
            var cachedCategory = await cacheService.GetAsync<CategoryIdDto>(categoryKey);

            if (cachedCategory is null)
            {
                var category = await unitOfWork.Categories.GetByIdAsync(request.CategoryId, cancellationToken);
                if (category is null)
                    return Result<IEnumerable<ProductResponseDto>>.Success(Enumerable.Empty<ProductResponseDto>());

                cachedCategory = mapper.Map<CategoryIdDto>(category);
                await cacheService.SetAsync(categoryKey, cachedCategory);
            }

            string productsKey = CacheKeys.ProductsByCategory(cachedCategory.Name);

            var cachedProducts = await cacheService.GetAsync<IEnumerable<ProductResponseDto>>(productsKey);
            if (cachedProducts != null)
                return Result<IEnumerable<ProductResponseDto>>.Success(cachedProducts);

            var products = await unitOfWork.Products.FindAsync(p => p.CategoryId == request.CategoryId);
            if (products is null || !products.Any())
                return Result<IEnumerable<ProductResponseDto>>.Success(Enumerable.Empty<ProductResponseDto>());

            var productsModels = mapper.Map<IEnumerable<ProductResponseDto>>(products);
            await cacheService.SetAsync(productsKey, productsModels);

            return Result<IEnumerable<ProductResponseDto>>.Success(productsModels);
        }

    }
}
