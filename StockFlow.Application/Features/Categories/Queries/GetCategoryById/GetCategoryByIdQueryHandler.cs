using AutoMapper;
using MediatR;
using StockFlow.Application.Cache;
using StockFlow.Application.Common.Constants;
using StockFlow.Application.Interfaces;
using StockFlow.Domain.Entities;
using StockFlow.Domain.Repositories;

namespace StockFlow.Application.Features.Categories.Queries.GetCategoryById
{
    public class GetCategoryByIdQueryHandler(
        IUnitOfWork unitOfWork, 
        ICacheService cache, 
        IMapper mapper) : IRequestHandler<GetCategoryByIdQuery, Result<CategoryIdDto>>
    {
        public async Task<Result<CategoryIdDto>> Handle(GetCategoryByIdQuery request, 
            CancellationToken cancellationToken)
        {
            string categoryKey = CacheKeys.CategoryById(request.Id);

            var categoryCached = await cache.GetAsync<CategoryIdDto>(categoryKey);

            if (categoryCached != null)
            {
                return Result<CategoryIdDto>.Success(categoryCached);
            }

            var category = await unitOfWork.Categories.GetByIdAsync(request.Id, cancellationToken);

            if (category is null)
            {
                return Result<CategoryIdDto>.Failure($"Category with ID {request.Id} not found.");
            }

            var categoryModel = mapper.Map<CategoryIdDto>(category);
            await cache.SetAsync(categoryKey, categoryModel);

            return Result<CategoryIdDto>.Success(categoryModel);
        }
    }
}
