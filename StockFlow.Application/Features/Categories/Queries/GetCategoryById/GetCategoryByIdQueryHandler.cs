using AutoMapper;
using MediatR;
using StockFlow.Application.Common.Constants;
using StockFlow.Application.Interfaces;
using StockFlow.Domain.Entities;

namespace StockFlow.Application.Features.Categories.Queries.GetCategoryById
{
    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, CategoryDto>
    {
        private readonly IRepository<CategoryEntity> _repository;
        private readonly ICacheService _cache;
        private readonly IMapper _mapper;

        public GetCategoryByIdQueryHandler(IRepository<CategoryEntity> repository, ICacheService cache, IMapper mapper)
        {
            _repository = repository;
            _cache = cache;
            _mapper = mapper;
        }

        public async Task<CategoryDto> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            string categoryKey = CacheKeys.CategoryById(request.Id);

            var categoryCached = await _cache.GetAsync<CategoryDto>(categoryKey);

            if (categoryCached != null)
            {
                return categoryCached;
            }

            var category = await _repository.GetByIdAsync(request.Id);

            var customerModel = _mapper.Map<CategoryDto>(category);
            await _cache.SetAsync(categoryKey, customerModel);
            return customerModel;
        }
    }
}
