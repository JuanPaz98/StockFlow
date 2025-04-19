using AutoMapper;
using MediatR;
using SendGrid.Helpers.Errors.Model;
using StockFlow.Application.Common.Constants;
using StockFlow.Application.Features.Customer.Commands.UpdateCustomer;
using StockFlow.Application.Interfaces;
using StockFlow.Domain.Entities;

namespace StockFlow.Application.Features.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, CategoryDto>
    {
        private readonly IRepository<CategoryEntity> _repository;
        private readonly ICacheService _cacheService;
        private readonly IMapper _mapper;

        public UpdateCategoryCommandHandler(IRepository<CategoryEntity> repository, ICacheService cacheService, IMapper mapper)
        {
            _repository = repository;
            _cacheService = cacheService;
            _mapper = mapper;
        }

        public async Task<CategoryDto> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _repository.GetByIdAsync(request.Model.Id);

            if (category is null)
            {
                throw new NotFoundException($"Category with ID {request.Model.Id} not found.");
            }

            _mapper.Map(request.Model, category);

            _repository.Update(category);
            await _repository.SaveChangesAsync();

            await _cacheService.RemoveAsync(CacheKeys.AllCategories);
            await _cacheService.RemoveAsync(CacheKeys.CategoryById(request.Model.Id));

            return _mapper.Map<CategoryDto>(category);
        }
    }
}
