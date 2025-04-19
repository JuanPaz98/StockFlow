using AutoMapper;
using MediatR;
using SendGrid.Helpers.Errors.Model;
using StockFlow.Api.Domain.Entities;
using StockFlow.Application.Common.Constants;
using StockFlow.Application.Interfaces;
using StockFlow.Domain.Entities;

namespace StockFlow.Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, UpdateProductModel>
    {
        private readonly IRepository<ProductEntity> _repository;
        private readonly IRepository<CategoryEntity> _categoriesRepository;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;

        public UpdateProductCommandHandler(
            IRepository<ProductEntity> repository,
            IRepository<CategoryEntity> categoriesRepository,
            IMapper mapper, 
            ICacheService cache)
        {
            _repository = repository;
            _categoriesRepository = categoriesRepository;
            _mapper = mapper;
            _cacheService = cache;
        }

        public async Task<UpdateProductModel> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIdAsync(request.Model.Id);

            if (product is null)
            {
                throw new NotFoundException($"Product with ID {request.Model.Id} not found.");
            }

            // Last category cache
            var lastCachedCategory = await _cacheService.GetAsync<CategoryDto>(CacheKeys.CategoryById(product.CategoryId));
            if (lastCachedCategory != null)
            {
                await _cacheService.RemoveAsync(CacheKeys.ProductsByCategory(lastCachedCategory.Name));
            }

            // New category cache
            var category = await _categoriesRepository.GetByIdAsync(request.Model.CategoryId);
            if (category != null)
            {
                await _cacheService.RemoveAsync(CacheKeys.ProductsByCategory(category.Name));
            }

            _mapper.Map(request.Model, product);

            _repository.Update(product);
            await _repository.SaveChangesAsync();
            await _cacheService.RemoveAsync(CacheKeys.AllProducts);
            await _cacheService.RemoveAsync(CacheKeys.ProductById(request.Model.Id));

            return _mapper.Map<UpdateProductModel>(product);
        }
    }
}
