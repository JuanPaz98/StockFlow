using AutoMapper;
using MediatR;
using SendGrid.Helpers.Errors.Model;
using StockFlow.Api.Domain.Entities;
using StockFlow.Application.Common.Constants;
using StockFlow.Application.Interfaces;

namespace StockFlow.Application.Features.Products.Queries.GetProductByCategory
{
    public class GetProductByCategoryQueryHandler : IRequestHandler<GetProductsByCategoryQuery,IEnumerable<GetProductByCategoryModel>>
    {
        private readonly IRepository<ProductEntity> _repository;
        private readonly ICacheService _cache;
        private readonly IMapper _mapper;

        public GetProductByCategoryQueryHandler(IRepository<ProductEntity> repository, ICacheService cache, IMapper mapper)
        {
            _repository = repository;
            _cache = cache;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetProductByCategoryModel>> Handle(GetProductsByCategoryQuery request, CancellationToken cancellationToken)
        {
            string productsKey = CacheKeys.ProductsByCategory(request.category);

            var cachedProducts = await _cache.GetAsync<IEnumerable<GetProductByCategoryModel>>(productsKey);

            if(cachedProducts != null)
            {
                return cachedProducts;
            }

            var products = await _repository.FindAsync(p =>p.Category == request.category);

            if (products is null || !products.Any())
            {
                return Enumerable.Empty<GetProductByCategoryModel>();
            }

            var productsModels = _mapper.Map<IEnumerable<GetProductByCategoryModel>>(products);

            await _cache.SetAsync(productsKey, productsModels);
            return productsModels;
        }
    }
}
