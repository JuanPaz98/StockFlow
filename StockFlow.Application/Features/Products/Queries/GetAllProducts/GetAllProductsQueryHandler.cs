using AutoMapper;
using MediatR;
using StockFlow.Api.Domain.Entities;
using StockFlow.Application.Common.Constants;
using StockFlow.Application.Interfaces;

namespace StockFlow.Application.Features.Products.Queries.GetAllProducts
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<GetAllProductsModel>>
    {
        private readonly IRepository<ProductEntity> _repository;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;

        public GetAllProductsQueryHandler(IRepository<ProductEntity> repository, IMapper mapper, ICacheService cache)
        {
            _repository = repository;
            _mapper = mapper;
            _cacheService = cache;
        }

        public async Task<IEnumerable<GetAllProductsModel>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var productsKey = CacheKeys.AllProducts;

            var cachedProducts = await _cacheService.GetAsync<IEnumerable<GetAllProductsModel>>(productsKey);

            if (cachedProducts != null)
            {
                return cachedProducts;
            }

            var products = await _repository.GetAllAsync();
            var productsModels = _mapper.Map<IEnumerable<GetAllProductsModel>>(products);

            await _cacheService.SetAsync(CacheKeys.AllProducts, productsModels);

            return productsModels;
        }
    }
}
