using AutoMapper;
using MediatR;
using StockFlow.Api.Domain.Entities;
using StockFlow.Application.Interfaces;

namespace StockFlow.Application.Features.Products.Queries.GetProductById
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, GetProductByIdModel>
    {
        private readonly IRepository<ProductEntity> _repository;
        private readonly IMapper _mapper;

        public GetProductByIdQueryHandler(IRepository<ProductEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GetProductByIdModel> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIdAsync(request.id);

            return _mapper.Map<GetProductByIdModel>(product);
        }
    }
}
