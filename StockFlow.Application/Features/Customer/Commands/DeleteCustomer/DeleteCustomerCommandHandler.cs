using MediatR;
using SendGrid.Helpers.Errors.Model;
using StockFlow.Api.Domain.Entities;
using StockFlow.Application.Interfaces;

namespace StockFlow.Application.Features.Customer.Commands.DeleteCustomer
{
    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, bool>
    {
        private readonly IRepository<CustomerEntity> _repository;

        public DeleteCustomerCommandHandler(IRepository<CustomerEntity> repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _repository.GetByIdAsync(request.id);

            if (customer is null)
            {
                throw new NotFoundException($"Customer with ID {request.id} not found.");
            }

            _repository.Remove(customer);
            await _repository.SaveChangesAsync();

            return true;
        }
    }
}
