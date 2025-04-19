using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using StockFlow.Application.Common.Constants;
using StockFlow.Application.Interfaces;
using StockFlow.Domain.Entities;

namespace StockFlow.Application.Features.Payments.Queries.GetPaymentsByOrderId
{
    class GetPaymentsByOrderIdQueryHandler : IRequestHandler<GetPaymentsByOrderIdQuery, IEnumerable<GetPaymentsByOrderIdModel>>
    {
        private readonly IRepository<PaymentEntity> _repository;
        private readonly ICacheService _cacheService;
        private readonly IMapper _mapper;

        public GetPaymentsByOrderIdQueryHandler(IRepository<PaymentEntity> repository, ICacheService cacheService, IMapper mapper)
        {
            _repository = repository;
            _cacheService = cacheService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetPaymentsByOrderIdModel>> Handle(GetPaymentsByOrderIdQuery request, CancellationToken cancellationToken)
        {
            string paymentKey = CacheKeys.PaymentsByOrderId(request.Id);

            var cachedPayments = await _cacheService.GetAsync<IEnumerable<GetPaymentsByOrderIdModel>>(paymentKey);

            if (cachedPayments != null) {
                return cachedPayments;
            }

            var payments = await _repository.FindAsync(p => p.OrderId  == request.Id);
            if (payments is null)
            {
                return Enumerable.Empty<GetPaymentsByOrderIdModel>();                
            }

            var paymentsModels = _mapper.Map<IEnumerable<GetPaymentsByOrderIdModel>>(payments);
            await _cacheService.SetAsync(paymentKey, paymentsModels);

            return paymentsModels;
        }
    }
}
