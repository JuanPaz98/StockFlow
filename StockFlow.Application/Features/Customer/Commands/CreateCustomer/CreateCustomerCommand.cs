using MediatR;
using StockFlow.Application.Features.Customer.Commands.CreateCustomer;


public record CreateCustomerCommand(CreateCustomerModel model) : IRequest<CreateCustomerModel>;

