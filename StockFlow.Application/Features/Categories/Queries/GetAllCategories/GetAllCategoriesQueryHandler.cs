using AutoMapper;
using MediatR;
using StockFlow.Application.Common.Constants;
using StockFlow.Application.Interfaces;
using StockFlow.Domain.Entities;

namespace StockFlow.Application.Features.Categories.Queries.GetAllCategories
{
    class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, IEnumerable<CategoryDto>>
    {
        private readonly IRepository<CategoryEntity> _repository;
        private readonly ICacheService _cacheService;
        private readonly IMapper _mapper;

        public GetAllCategoriesQueryHandler(IRepository<CategoryEntity> repository, ICacheService cacheService, IMapper mapper)
        {
            _repository = repository;
            _cacheService = cacheService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryDto>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            string categoriesKey = CacheKeys.AllCategories;

            var cachedCategories = await _cacheService.GetAsync<IEnumerable<CategoryDto>>(categoriesKey);

            if (cachedCategories != null)
            {
                return cachedCategories;
            }

            var categories = await _repository.GetAllAsync();
            if (!categories.Any())
            {
                return Enumerable.Empty<CategoryDto>();
            }

            var categoriesModels = _mapper.Map<IEnumerable<CategoryDto>>(categories);
            await _cacheService.SetAsync(categoriesKey, categoriesModels);

            return categoriesModels;
        }
    }
}
