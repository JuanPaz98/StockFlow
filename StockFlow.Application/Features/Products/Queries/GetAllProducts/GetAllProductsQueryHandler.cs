using AutoMapper;
using MediatR;
using StockFlow.Api.Domain.Entities;
using StockFlow.Application.Interfaces;

namespace StockFlow.Application.Features.Products.Queries.GetAllProducts
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<GetAllProductsModel>>
    {
        private readonly IRepository<ProductEntity> _repository;
        private readonly IMapper _mapper;

        public GetAllProductsQueryHandler(IRepository<ProductEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetAllProductsModel>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _repository.GetAllAsync();

            return _mapper.Map<IEnumerable<GetAllProductsModel>>(products);
        }
    }
}
