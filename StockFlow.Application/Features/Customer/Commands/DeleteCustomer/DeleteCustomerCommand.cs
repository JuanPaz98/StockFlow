
using MediatR;

public record DeleteCustomerCommand(int id): IRequest<bool>;
    
