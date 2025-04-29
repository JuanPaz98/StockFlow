using MediatR;
using StockFlow.Application.Features.Dtos.Customers;

namespace StockFlow.Application.Features.Customer.Commands.UpdateCustomer
{
    public record UpdateCustomerCommand(CustomerRequestIdDto Data) : IRequest<Result<CustomerRequestIdDto>>;
}
