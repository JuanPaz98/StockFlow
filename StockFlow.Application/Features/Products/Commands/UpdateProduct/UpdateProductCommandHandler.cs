using AutoMapper;
using MediatR;
using StockFlow.Application.Cache;
using StockFlow.Application.Common.Constants;
using StockFlow.Application.Features.Dtos.Products;
using StockFlow.Application.Interfaces;

namespace StockFlow.Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ICacheService cacheService) : IRequestHandler<UpdateProductCommand, Result<ProductRequestIdDto>>
    {
        public async Task<Result<ProductRequestIdDto>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await unitOfWork.Products.GetByIdAsync(request.Data.Id, cancellationToken);

            if (product is null)
            {
                return Result<ProductRequestIdDto>.Failure($"Product with ID {request.Data.Id} not found.");
            }

            // Last category cache
            var lastCachedCategory = await cacheService.GetAsync<CategoryIdDto>(CacheKeys.CategoryById(product.CategoryId));
            if (lastCachedCategory is not null)
            {
                await cacheService.RemoveAsync(CacheKeys.ProductsByCategory(lastCachedCategory.Name));
            }

            // New category cache
            var category = await unitOfWork.Categories.GetByIdAsync(request.Data.CategoryId, cancellationToken);
            if (category is not null)
            {
                await cacheService.RemoveAsync(CacheKeys.ProductsByCategory(category.Name));
            }

            mapper.Map(request.Data, product);

            unitOfWork.Products.Update(product);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            await cacheService.RemoveAsync(CacheKeys.AllProducts);
            await cacheService.RemoveAsync(CacheKeys.ProductById(request.Data.Id));

            return Result<ProductRequestIdDto>.Success(mapper.Map<ProductRequestIdDto>(product));
        }
    }
}
