using MediatR;
using StockFlow.Application.Interfaces;

namespace StockFlow.Application.Features.Customer.Commands.DeleteCustomer
{
    public class DeleteCustomerCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteCustomerCommand, Result<bool>>
    {
        public async Task<Result<bool>> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await unitOfWork.Customers.GetByIdAsync(request.Id, cancellationToken);
            if (customer is null)
            {
                return Result<bool>.Failure($"Customer with ID {request.Id} not found.");
            }

            unitOfWork.Customers.Remove(customer);

            var saveResult = await unitOfWork.SaveChangesAsync(cancellationToken) > 0;

            if (!saveResult)
            {
                return Result<bool>.Failure("An error occurred while deleting the customer.");
            }

            return Result<bool>.Success(saveResult);
        }
    }
}
