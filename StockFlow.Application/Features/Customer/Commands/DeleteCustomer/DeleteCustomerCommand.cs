
using MediatR;

public record DeleteCustomerCommand(int Id): IRequest<Result<bool>>;
    
