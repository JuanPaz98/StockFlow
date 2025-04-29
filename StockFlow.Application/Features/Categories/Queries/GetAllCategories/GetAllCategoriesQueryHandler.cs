using AutoMapper;
using MediatR;
using StockFlow.Application.Cache;
using StockFlow.Application.Common.Constants;
using StockFlow.Application.Interfaces;

namespace StockFlow.Application.Features.Categories.Queries.GetAllCategories
{
    class GetAllCategoriesQueryHandler(
        IUnitOfWork unitOfWork,
        ICacheService cacheService,
        IMapper mapper) : IRequestHandler<GetAllCategoriesQuery, Result<IEnumerable<CategoryIdDto>>>
    {
        public async Task<Result<IEnumerable<CategoryIdDto>>> Handle(GetAllCategoriesQuery request,
            CancellationToken cancellationToken)
        {
            string categoriesKey = CacheKeys.AllCategories;

            var cachedCategories = await cacheService.GetAsync<IEnumerable<CategoryIdDto>>(categoriesKey);

            if (cachedCategories != null)
            {
                return Result<IEnumerable<CategoryIdDto>>.Success(cachedCategories);
            }

            var categories = await unitOfWork.Categories.GetAllAsync(cancellationToken);
            var categoriesModels = mapper.Map<IEnumerable<CategoryIdDto>>(categories);
            await cacheService.SetAsync(categoriesKey, categoriesModels);

            return Result<IEnumerable<CategoryIdDto>>.Success(categoriesModels);
        }
    }
}
