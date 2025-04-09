using AutoMapper;
using MediatR;
using StockFlow.Api.Domain.Entities;
using StockFlow.Application.Common.Constants;
using StockFlow.Application.Interfaces;

namespace StockFlow.Application.Features.Suppliers.Commands.CreateSupplier
{
    public class CreateSupplierCommandHandler: IRequestHandler<CreateSupplierCommand, int>
    {
        private readonly IRepository<SupplierEntity> _repository;
        private readonly ICacheService _cache;
        private readonly IMapper _mapper;

        public CreateSupplierCommandHandler(
            IRepository<SupplierEntity> repository, 
            ICacheService cache, 
            IMapper mapper)
        {
            _repository = repository;
            _cache = cache;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateSupplierCommand request, CancellationToken cancellationToken)
        {
            var supplierEntity = _mapper.Map<SupplierEntity>(request.Model);

            await _repository.AddAsync(supplierEntity);

            await _cache.RemoveAsync(CacheKeys.AllSuppliers);

            return await _repository.SaveChangesAsync();
        }
    }
}
