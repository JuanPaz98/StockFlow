using AutoMapper;
using MediatR;
using SendGrid.Helpers.Errors.Model;
using StockFlow.Api.Domain.Entities;
using StockFlow.Application.Common.Constants;
using StockFlow.Application.Interfaces;
using StockFlow.Domain.Entities;

namespace StockFlow.Application.Features.Products.Queries.GetProductByCategory
{
    public class GetProductByCategoryQueryHandler : IRequestHandler<GetProductsByCategoryQuery, IEnumerable<GetProductByCategoryModel>>
    {
        private readonly IRepository<ProductEntity> _productsRepository;
        private readonly IRepository<CategoryEntity> _categoriesRepository;
        private readonly ICacheService _cacheService;
        private readonly IMapper _mapper;

        public GetProductByCategoryQueryHandler(IRepository<ProductEntity> repository, ICacheService cache, IMapper mapper, IRepository<CategoryEntity> categoriesRepository)
        {
            _productsRepository = repository;
            _categoriesRepository = categoriesRepository;
            _cacheService = cache;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetProductByCategoryModel>> Handle(GetProductsByCategoryQuery request, CancellationToken cancellationToken)
        {
            string categoryKey = CacheKeys.CategoryById(request.categoryId);
            var cachedCategory = await _cacheService.GetAsync<CategoryDto>(categoryKey);

            if (cachedCategory is null)
            {
                var category = await _categoriesRepository.GetByIdAsync(request.categoryId);
                if (category is null)
                    return Enumerable.Empty<GetProductByCategoryModel>();

                cachedCategory = _mapper.Map<CategoryDto>(category);
                await _cacheService.SetAsync(categoryKey, cachedCategory);
            }

            string productsKey = CacheKeys.ProductsByCategory(cachedCategory.Name);

            var cachedProducts = await _cacheService.GetAsync<IEnumerable<GetProductByCategoryModel>>(productsKey);
            if (cachedProducts != null)
                return cachedProducts;

            var products = await _productsRepository.FindAsync(p => p.CategoryId == request.categoryId);
            if (products is null || !products.Any())
                return Enumerable.Empty<GetProductByCategoryModel>();

            var productsModels = _mapper.Map<IEnumerable<GetProductByCategoryModel>>(products);
            await _cacheService.SetAsync(productsKey, productsModels);

            return productsModels;
        }

    }
}
