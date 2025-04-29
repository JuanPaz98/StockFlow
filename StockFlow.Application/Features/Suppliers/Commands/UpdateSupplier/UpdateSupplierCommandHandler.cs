using AutoMapper;
using MediatR;
using SendGrid.Helpers.Errors.Model;
using StockFlow.Api.Domain.Entities;
using StockFlow.Application.Cache;
using StockFlow.Application.Common.Constants;
using StockFlow.Application.Features.Dtos.Suppliers;
using StockFlow.Application.Interfaces;

namespace StockFlow.Application.Features.Suppliers.Commands.UpdateSupplier
{
    public class UpdateSupplierCommandHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ICacheService cacheService) : IRequestHandler<UpdateSupplierCommad, Result<SupplierRequestIdDto>>
    {
        public async Task<Result<SupplierRequestIdDto>> Handle(UpdateSupplierCommad request, CancellationToken cancellationToken)
        {
            var supplier = await unitOfWork.Suppliers.GetByIdAsync(request.Data.Id, cancellationToken);

            if (supplier is null)
            {
                return Result<SupplierRequestIdDto>.Failure($"Supplier with ID {request.Data.Id} not found.");
            }

            mapper.Map(request.Data, supplier);

            unitOfWork.Suppliers.Update(supplier);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            await cacheService.RemoveAsync(CacheKeys.AllSuppliers);
            await cacheService.RemoveAsync(CacheKeys.SupplierById(request.Data.Id));

            return Result<SupplierRequestIdDto>.Success(mapper.Map<SupplierRequestIdDto>(supplier));
        }
    }
}
