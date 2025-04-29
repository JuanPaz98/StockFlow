using AutoMapper;
using MediatR;
using SendGrid.Helpers.Errors.Model;
using StockFlow.Application.Cache;
using StockFlow.Application.Common.Constants;
using StockFlow.Application.Interfaces;

namespace StockFlow.Application.Features.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommandHandler(
        IUnitOfWork unitOfWork, 
        ICacheService cacheService, 
        IMapper mapper) : IRequestHandler<UpdateCategoryCommand, Result<CategoryIdDto>>
    {
        public async Task<Result<CategoryIdDto>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await unitOfWork.Categories.GetByIdAsync(request.Data.Id, cancellationToken);

            if (category is null)
            {
                return Result<CategoryIdDto>.Failure($"Category with ID {request.Data.Id} not found.");
            }

            mapper.Map(request.Data, category);
            unitOfWork.Categories.Update(category);
            var saveResult = await unitOfWork.SaveChangesAsync(cancellationToken) > 0;

            if (!saveResult)
            {
                return Result<CategoryIdDto>.Failure("An error occurred while updating the category.");
            }

            await cacheService.RemoveAsync(CacheKeys.AllCategories);
            await cacheService.RemoveAsync(CacheKeys.CategoryById(request.Data.Id));

            return Result<CategoryIdDto>.Success(mapper.Map<CategoryIdDto>(category));
        }
    }
}
