using AutoMapper;
using MediatR;
using StockFlow.Application.Cache;
using StockFlow.Application.Common.Constants;
using StockFlow.Application.Interfaces;
using StockFlow.Domain.Entities;

namespace StockFlow.Application.Features.Payments.Commands.CreatePayment
{
    public class CreatePaymentCommandHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ICacheService cacheService) : IRequestHandler<CreatePaymentCommand, Result<int>>
    {
        public async Task<Result<int>> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
        {
            var paymentEntity = mapper.Map<PaymentEntity>(request.Data);

            paymentEntity.PaymentDate = DateTime.Now;

            await unitOfWork.Payments.AddAsync(paymentEntity, cancellationToken);

            await cacheService.RemoveAsync(CacheKeys.PaymentById(paymentEntity.Id));

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<int>.Success(paymentEntity.Id);
        }
    }
}
