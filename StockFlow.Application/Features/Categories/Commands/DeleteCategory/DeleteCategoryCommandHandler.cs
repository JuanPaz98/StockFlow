using MediatR;
using StockFlow.Application.Cache;
using StockFlow.Application.Common.Constants;
using StockFlow.Application.Interfaces;

namespace StockFlow.Application.Features.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryCommandHandler(IUnitOfWork unitOfWork, ICacheService cacheService) : IRequestHandler<DeleteCategoryCommand, Result<bool>>
    {
        public async Task<Result<bool>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await unitOfWork.Categories.GetByIdAsync(request.Id, cancellationToken);
            if (category == null)
            {
                return Result<bool>.Failure("Category not found");
            }

            unitOfWork.Categories.Remove(category);

            var saveResult = await unitOfWork.SaveChangesAsync(cancellationToken) > 0;

            await cacheService.RemoveAsync(CacheKeys.AllCategories);
            await cacheService.RemoveAsync(CacheKeys.CategoryById(request.Id));

            return Result<bool>.Success(saveResult);
        }
    }
}
