using AutoMapper;
using MediatR;
using StockFlow.Api.Domain.Entities;
using StockFlow.Application.Common.Constants;
using StockFlow.Application.Interfaces;
using StockFlow.Domain.Entities;

namespace StockFlow.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
    {
        private readonly IRepository<ProductEntity> _productsRepository;
        private readonly IRepository<CategoryEntity> _categoriesRepository;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;

        public CreateProductCommandHandler(
            IRepository<ProductEntity> repository,
            IRepository<CategoryEntity> categoriesRepository,
            IMapper mapper, 
            ICacheService cache)
        {
            _productsRepository = repository;
            _categoriesRepository = categoriesRepository;
            _mapper = mapper;
            _cacheService = cache;
        }

        public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var productEntity = _mapper.Map<ProductEntity>(request.Model);

            var cachedCategory = await _cacheService.GetAsync<CategoryDto>(CacheKeys.CategoryById(request.Model.CategoryId));
            if (cachedCategory != null)
            {
                await _cacheService.RemoveAsync(CacheKeys.ProductsByCategory(cachedCategory.Name));
            }
            else
            {
                var category = await _categoriesRepository.GetByIdAsync(request.Model.CategoryId);
                if (category != null)
                {
                    var categoryDto = _mapper.Map<CategoryDto>(category);
                    await _cacheService.SetAsync(CacheKeys.CategoryById(request.Model.CategoryId), categoryDto);
                    await _cacheService.RemoveAsync(CacheKeys.ProductsByCategory(categoryDto.Name));
                }
            }

            await _productsRepository.AddAsync(productEntity);

            return await _productsRepository.SaveChangesAsync();
        }
    }
}
