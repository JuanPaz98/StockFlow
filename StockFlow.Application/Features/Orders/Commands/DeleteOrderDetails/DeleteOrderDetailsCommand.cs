
using MediatR;

public record DeleteOrderDetailsCommand(int Id): IRequest<Result<bool>>;

