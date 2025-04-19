using AutoMapper;
using MediatR;
using StockFlow.Api.Domain.Entities;
using StockFlow.Application.Common.Constants;
using StockFlow.Application.Interfaces;

namespace StockFlow.Application.Features.Suppliers.Queries.GetSupplierById
{
    public class GetSupplierByIdQueryHandler : IRequestHandler<GetSupplierByIdQuery, GetSupplierByIdModel>
    {
        private readonly IRepository<SupplierEntity> _repository;
        private readonly ICacheService _cacheService;
        private readonly IMapper _mapper;

        public GetSupplierByIdQueryHandler(IRepository<SupplierEntity> repository, ICacheService cache, IMapper mapper)
        {
            _repository = repository;
            _cacheService = cache;
            _mapper = mapper;
        }

        public async Task<GetSupplierByIdModel> Handle(GetSupplierByIdQuery request, CancellationToken cancellationToken)
        {
            var supplierKey = CacheKeys.SupplierById(request.Id);
            var supplierCached = await _cacheService.GetAsync<GetSupplierByIdModel>(supplierKey);

            if(supplierCached !=  null)
            {
                return supplierCached;
            }

            var supplier = await _repository.GetByIdAsync(request.Id);

            var supplierModel = _mapper.Map<GetSupplierByIdModel>(supplier);

            await _cacheService.SetAsync(supplierKey, supplierModel);

            return supplierModel;
        }
    }
}
