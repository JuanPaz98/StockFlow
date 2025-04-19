using AutoMapper;
using MediatR;
using SendGrid.Helpers.Errors.Model;
using StockFlow.Api.Domain.Entities;
using StockFlow.Application.Common.Constants;
using StockFlow.Application.Interfaces;

namespace StockFlow.Application.Features.Suppliers.Queries.GetAllSuppliers
{
    public class GetAllSuppliersQueryHandler : IRequestHandler<GetAllSuppliersQuery, IEnumerable<GetAllSuppliersModel>>
    {
        private readonly IRepository<SupplierEntity> _repository;
        private readonly ICacheService _cacheService;
        private readonly IMapper _mapper;

        public GetAllSuppliersQueryHandler(IRepository<SupplierEntity> repository, ICacheService cache, IMapper mapper)
        {
            _repository = repository;
            _cacheService = cache;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetAllSuppliersModel>> Handle(GetAllSuppliersQuery request, CancellationToken cancellationToken)
        {
            var suppliersKey = CacheKeys.AllSuppliers;

            var cachedSuppliers = await _cacheService.GetAsync<IEnumerable<GetAllSuppliersModel>>(suppliersKey);

            if(cachedSuppliers != null)
            {
                return cachedSuppliers;
            }

            var suppliers = await _repository.GetAllAsync();

            if (!suppliers.Any())
            {
                return Enumerable.Empty<GetAllSuppliersModel>();
            }

            var suppliersModels = _mapper.Map<IEnumerable<GetAllSuppliersModel>>(suppliers);

            await _cacheService.SetAsync(suppliersKey, suppliersModels);

            return suppliersModels;
        }
    }
}
