using AutoMapper;
using MediatR;
using StockFlow.Application.Cache;
using StockFlow.Application.Common.Constants;
using StockFlow.Application.Features.Dtos.Payments;
using StockFlow.Application.Interfaces;

namespace StockFlow.Application.Features.Payments.Queries.GetPaymentsByOrderId
{
    class GetPaymentsByOrderIdQueryHandler(
        IUnitOfWork unitOfWork,
        ICacheService cacheService,
        IMapper mapper) : IRequestHandler<GetPaymentsByOrderIdQuery, Result<IEnumerable<PaymentResponseDto>>>
    {
        public async Task<Result<IEnumerable<PaymentResponseDto>>> Handle(GetPaymentsByOrderIdQuery request, CancellationToken cancellationToken)
        {
            string paymentKey = CacheKeys.PaymentsByOrderId(request.Id);

            var cachedPayments = await cacheService.GetAsync<IEnumerable<PaymentResponseDto>>(paymentKey);

            if (cachedPayments is not null)
            {
                return Result<IEnumerable<PaymentResponseDto>>.Success(cachedPayments);
            }

            var payments = await unitOfWork.Payments.FindAsync(p => p.OrderId == request.Id, cancellationToken);

            if (!payments.Any())
            {
                return Result<IEnumerable<PaymentResponseDto>>.Success(Enumerable.Empty<PaymentResponseDto>());
            }

            var paymentsModels = mapper.Map<IEnumerable<PaymentResponseDto>>(payments);
            await cacheService.SetAsync(paymentKey, paymentsModels);

            return Result<IEnumerable<PaymentResponseDto>>.Success(paymentsModels);
        }
    }
}
