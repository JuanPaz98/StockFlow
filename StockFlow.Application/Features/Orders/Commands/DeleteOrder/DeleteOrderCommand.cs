
using MediatR;

public record DeleteOrderCommand(int Id) : IRequest<Result<bool>>;
