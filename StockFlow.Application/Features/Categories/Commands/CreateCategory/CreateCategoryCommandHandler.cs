using AutoMapper;
using MediatR;
using StockFlow.Application.Common.Constants;
using StockFlow.Application.Interfaces;
using StockFlow.Domain.Entities;

namespace StockFlow.Application.Features.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, int>
    {
        private readonly IRepository<CategoryEntity> _repository;
        private readonly ICacheService _cacheService;
        private readonly IMapper _mapper;

        public CreateCategoryCommandHandler(IRepository<CategoryEntity> repository, ICacheService cacheService, IMapper mapper)
        {
            _repository = repository;
            _cacheService = cacheService;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var categoryEntity = _mapper.Map<CategoryEntity>(request.Model);
            if (categoryEntity == null) 
            {
                throw new ArgumentNullException(nameof(categoryEntity));
            }

            await _repository.AddAsync(categoryEntity);
            await _cacheService.RemoveAsync(CacheKeys.AllCategories);

            return await _repository.SaveChangesAsync();
        }
    }
}
