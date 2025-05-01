using AutoMapper;
using MediatR;
using StockFlow.Api.Domain.Entities;
using StockFlow.Application.Cache;
using StockFlow.Application.Common.Constants;
using StockFlow.Application.Interfaces;

namespace StockFlow.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ICacheService cacheService) : IRequestHandler<CreateProductCommand, Result<int>>
    {
        public async Task<Result<int>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var productEntity = mapper.Map<ProductEntity>(request.Data);

            var cachedCategory = await cacheService.GetAsync<CategoryIdDto>(CacheKeys.CategoryById(request.Data.CategoryId));
            if (cachedCategory is not null)
            {
                await cacheService.RemoveAsync(CacheKeys.ProductsByCategory(cachedCategory.Name));
            }
            else
            {
                var category = await unitOfWork.Categories.GetByIdAsync(request.Data.CategoryId, cancellationToken);
                if (category is not null)
                {
                    var categoryDto = mapper.Map<CategoryIdDto>(category);
                    await cacheService.SetAsync(CacheKeys.CategoryById(request.Data.CategoryId), categoryDto);
                    await cacheService.RemoveAsync(CacheKeys.ProductsByCategory(categoryDto.Name));
                }
            }

            await unitOfWork.Products.AddAsync(productEntity, cancellationToken);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            await cacheService.RemoveAsync(CacheKeys.AllProducts);

            return Result<int>.Success(productEntity.Id);
        }
    }
}
