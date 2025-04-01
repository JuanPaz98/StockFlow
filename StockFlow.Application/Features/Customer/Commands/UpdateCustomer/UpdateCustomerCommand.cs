using MediatR;

namespace StockFlow.Application.Features.Customer.Commands.UpdateCustomer
{
    public record UpdateCustomerCommand(UpdateCustomerModel model) : IRequest<UpdateCustomerModel>;
}
