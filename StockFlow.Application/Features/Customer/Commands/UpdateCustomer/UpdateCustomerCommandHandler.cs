using AutoMapper;
using MediatR;
using SendGrid.Helpers.Errors.Model;
using StockFlow.Api.Domain.Entities;
using StockFlow.Application.Interfaces;

namespace StockFlow.Application.Features.Customer.Commands.UpdateCustomer
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, UpdateCustomerModel>
    {
        private readonly IRepository<CustomerEntity> _repository;
        private readonly IMapper _mapper;

        public UpdateCustomerCommandHandler(IRepository<CustomerEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<UpdateCustomerModel> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _repository.GetByIdAsync(request.model.Id);

            if (customer is null)
            {
                throw new NotFoundException($"Customer with ID {request.model.Id} not found.");
            }

            _mapper.Map(request.model, customer);

            _repository.Update(customer);
            await _repository.SaveChangesAsync();

            return _mapper.Map<UpdateCustomerModel>(customer);
        }
    }
}
