using AutoMapper;
using MediatR;
using StockFlow.Api.Domain.Entities;
using StockFlow.Application.Common.Constants;
using StockFlow.Application.Interfaces;

namespace StockFlow.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
    {
        private readonly IRepository<ProductEntity> _repository;
        private readonly IMapper _mapper;
        private readonly ICacheService _cache;

        public CreateProductCommandHandler(IRepository<ProductEntity> repository, IMapper mapper, ICacheService cache)
        {
            _repository = repository;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var productEntity = _mapper.Map<ProductEntity>(request.Model);
            await _repository.AddAsync(productEntity);
            await _cache.RemoveAsync(CacheKeys.AllProducts);
            await _cache.RemoveAsync(CacheKeys.ProductsByCategory(request.Model.Category));

            return await _repository.SaveChangesAsync();
        }
    }
}
