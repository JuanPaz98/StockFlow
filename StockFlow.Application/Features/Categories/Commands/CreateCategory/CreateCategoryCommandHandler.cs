using AutoMapper;
using MediatR;
using StockFlow.Application.Cache;
using StockFlow.Application.Common.Constants;
using StockFlow.Application.Interfaces;
using StockFlow.Domain.Entities;

namespace StockFlow.Application.Features.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommandHandler(
        IUnitOfWork unitOfWork,
        ICacheService cacheService,
        IMapper mapper) : IRequestHandler<CreateCategoryCommand, Result<int>>
    {
        public async Task<Result<int>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var categoryEntity = mapper.Map<CategoryEntity>(request.Data);

            var existingCategory = await unitOfWork.Categories.FindAsync(c => c.Name == categoryEntity.Name, cancellationToken);

            if(existingCategory.Any())
            {
                return Result<int>.Failure($"Category with name {categoryEntity.Name} already exists.");
            }

            await unitOfWork.Categories.AddAsync(categoryEntity, cancellationToken);
            await cacheService.RemoveAsync(CacheKeys.AllCategories);

            var saveResult = await unitOfWork.SaveChangesAsync(cancellationToken) > 0;

            if (!saveResult)
            {
                return Result<int>.Failure("An error occurred while creating the category.");
            }

            return Result<int>.Success(categoryEntity.Id);
        }
    }
}
