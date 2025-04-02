
using MediatR;

public record DeleteOrderCommand(int id) : IRequest<bool>;
