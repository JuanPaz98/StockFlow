using AutoMapper;
using MediatR;
using StockFlow.Api.Domain.Entities;
using StockFlow.Application.Interfaces;

namespace StockFlow.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
    {
        private readonly IRepository<ProductEntity> _repository;
        private readonly IMapper _mapper;

        public CreateProductCommandHandler(IRepository<ProductEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var productEntity = _mapper.Map<ProductEntity>(request.Model);
            await _repository.AddAsync(productEntity);

            return await _repository.SaveChangesAsync();
        }
    }
}
