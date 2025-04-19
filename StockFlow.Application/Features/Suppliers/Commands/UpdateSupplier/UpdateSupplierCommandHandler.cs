using AutoMapper;
using MediatR;
using SendGrid.Helpers.Errors.Model;
using StockFlow.Api.Domain.Entities;
using StockFlow.Application.Common.Constants;
using StockFlow.Application.Interfaces;

namespace StockFlow.Application.Features.Suppliers.Commands.UpdateSupplier
{
    public class UpdateSupplierCommandHandler : IRequestHandler<UpdateSupplierCommad, UpdateSupplierModel>
    {
        private readonly IRepository<SupplierEntity> _repository;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;

        public UpdateSupplierCommandHandler(
            IRepository<SupplierEntity> repository, 
            IMapper mapper, 
            ICacheService cache)
        {
            _repository = repository;
            _mapper = mapper;
            _cacheService = cache;
        }

        public async Task<UpdateSupplierModel> Handle(UpdateSupplierCommad request, CancellationToken cancellationToken)
        {
            var supplier = await _repository.GetByIdAsync(request.Model.Id);

            if (supplier is null)
            {
                throw new NotFoundException($"Supplier with ID {request.Model.Id} not found.");
            }

            _mapper.Map(request.Model, supplier);

            _repository.Update(supplier);
            await _repository.SaveChangesAsync();
            await _cacheService.RemoveAsync(CacheKeys.AllSuppliers);
            await _cacheService.RemoveAsync(CacheKeys.SupplierById(request.Model.Id));

            return _mapper.Map<UpdateSupplierModel>(supplier);
        }
    }
}
