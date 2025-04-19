using AutoMapper;
using MediatR;
using StockFlow.Application.Common.Constants;
using StockFlow.Application.Interfaces;
using StockFlow.Domain.Entities;

namespace StockFlow.Application.Features.Payments.Commands.CreatePayment
{
    public class CreatePaymentCommandHandler: IRequestHandler<CreatePaymentCommand, int>
    {
        private readonly IRepository<PaymentEntity> _repository;
        private readonly ICacheService _cacheService;
        private readonly IMapper _mapper;

        public CreatePaymentCommandHandler(IRepository<PaymentEntity> repository,  IMapper mapper, ICacheService cache)
        {
            _repository = repository;
            _mapper = mapper;
            _cacheService = cache;
        }

        public async Task<int> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
        {
            var paymentEntity = _mapper.Map<PaymentEntity>(request.Model);

            paymentEntity.PaymentDate = DateTime.Now;

            await _repository.AddAsync(paymentEntity);

            await _cacheService.RemoveAsync(CacheKeys.PaymentById(paymentEntity.Id));

            return await _repository.SaveChangesAsync();
        }
    }
}
