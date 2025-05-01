using MediatR;
using StockFlow.Application.Cache;
using StockFlow.Application.Common.Constants;
using StockFlow.Application.Interfaces;

namespace StockFlow.Application.Features.Suppliers.Commands.DeleteSupplier
{
    public class DeleteSupplierCommandHandler(
        IUnitOfWork unitOfWork,
        ICacheService cacheService) : IRequestHandler<DeleteSupplierCommand, Result<bool>>
    {
        public async Task<Result<bool>> Handle(DeleteSupplierCommand request, CancellationToken cancellationToken)
        {
            var supplier = await unitOfWork.Suppliers.GetByIdAsync(request.Id, cancellationToken);
            if (supplier is null)
            {
                return Result<bool>.Failure("Supplier not found");
            }

            unitOfWork.Suppliers.Remove(supplier);
            var saveResult = await unitOfWork.SaveChangesAsync(cancellationToken);

            await cacheService.RemoveAsync(CacheKeys.AllSuppliers);
            await cacheService.RemoveAsync(CacheKeys.SupplierById(request.Id));

            return Result<bool>.Success(saveResult > 0);
        }
    }
}
