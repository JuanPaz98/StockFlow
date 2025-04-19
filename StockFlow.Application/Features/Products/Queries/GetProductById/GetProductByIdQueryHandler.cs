using AutoMapper;
using MediatR;
using StockFlow.Api.Domain.Entities;
using StockFlow.Application.Common.Constants;
using StockFlow.Application.Interfaces;

namespace StockFlow.Application.Features.Products.Queries.GetProductById
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, GetProductByIdModel>
    {
        private readonly IRepository<ProductEntity> _repository;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;

        public GetProductByIdQueryHandler(IRepository<ProductEntity> repository, IMapper mapper, ICacheService cache)
        {
            _repository = repository;
            _mapper = mapper;
            _cacheService = cache;
        }

        public async Task<GetProductByIdModel> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var productKey = CacheKeys.ProductById(request.id);

            var cachedProduct = await _cacheService.GetAsync<GetProductByIdModel>(productKey);

            if(cachedProduct != null)
            {
                return cachedProduct;
            }

            var product = await _repository.GetByIdAsync(request.id);

            var productModel = _mapper.Map<GetProductByIdModel>(product);
            await _cacheService.SetAsync(productKey, productModel);

            return productModel;
        }
    }
}
