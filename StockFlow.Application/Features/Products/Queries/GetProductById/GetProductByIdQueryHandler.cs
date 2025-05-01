using AutoMapper;
using MediatR;
using StockFlow.Application.Cache;
using StockFlow.Application.Common.Constants;
using StockFlow.Application.Features.Dtos.Products;
using StockFlow.Application.Interfaces;

namespace StockFlow.Application.Features.Products.Queries.GetProductById
{
    public class GetProductByIdQueryHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ICacheService cacheService) : IRequestHandler<GetProductByIdQuery, Result<ProductResponseDto>>
    {
        public async Task<Result<ProductResponseDto>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var productKey = CacheKeys.ProductById(request.Id);

            var cachedProduct = await cacheService.GetAsync<ProductResponseDto>(productKey);

            if (cachedProduct is not null)
            {
                return Result<ProductResponseDto>.Success(cachedProduct);
            }

            var product = await unitOfWork.Products.GetByIdAsync(request.Id, cancellationToken);

            if(product is null)
            {
                return Result<ProductResponseDto>.Failure($"Product with ID {request.Id} not found");
            }

            var productModel = mapper.Map<ProductResponseDto>(product);
            await cacheService.SetAsync(productKey, productModel);

            return Result<ProductResponseDto>.Success(productModel);
        }
    }
}
