using AutoMapper;
using MediatR;
using SendGrid.Helpers.Errors.Model;
using StockFlow.Api.Domain.Entities;
using StockFlow.Application.Interfaces;

namespace StockFlow.Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, UpdateProductModel>
    {
        private readonly IRepository<ProductEntity> _repository;
        private readonly IMapper _mapper;

        public UpdateProductCommandHandler(IRepository<ProductEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<UpdateProductModel> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIdAsync(request.model.Id);

            if (product is null)
            {
                throw new NotFoundException($"Product with ID {request.model.Id} not found.");
            }

            _mapper.Map(request.model, product);

            _repository.Update(product);
            await _repository.SaveChangesAsync();

            return _mapper.Map<UpdateProductModel>(product);
        }
    }
}
