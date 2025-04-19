using AutoMapper;
using MediatR;
using StockFlow.Application.Interfaces;
using StockFlow.Domain.Entities;

namespace StockFlow.Application.Features.Payments.Commands.CreatePayment
{
    public class CreatePaymentCommandHandler: IRequestHandler<CreatePaymentCommand, int>
    {
        private readonly IRepository<PaymentEntity> _repository;
        private readonly IMapper _mapper;

        public CreatePaymentCommandHandler(IRepository<PaymentEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
        {
            var paymentEntity = _mapper.Map<PaymentEntity>(request.Model);

            await _repository.AddAsync(paymentEntity);

            return await _repository.SaveChangesAsync();
        }
    }
}
