using MediatR;
using StockFlow.Application.Cache;
using StockFlow.Application.Common.Constants;
using StockFlow.Application.Interfaces;

namespace StockFlow.Application.Features.Products.Commands.DeleteProduct
{
    public class DeleteProductCommandHandler(
        IUnitOfWork unitOfWork,
        ICacheService cacheService) : IRequestHandler<DeleteProductCommand, Result<bool>>
    {
        public async Task<Result<bool>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await unitOfWork.Products.GetByIdAsync(request.Id, cancellationToken);

            if (product is null)
            {
                return Result<bool>.Failure("Product not found");
            }

            unitOfWork.Products.Remove(product);

            var saveResult = await unitOfWork.SaveChangesAsync(cancellationToken);
            await cacheService.RemoveAsync(CacheKeys.AllProducts);
            await cacheService.RemoveAsync(CacheKeys.ProductById(request.Id));

            return Result<bool>.Success(saveResult > 0);
        }
    }
}
