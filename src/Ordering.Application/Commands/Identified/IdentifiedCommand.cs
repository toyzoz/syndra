using MediatR;

namespace Ordering.Application.Commands.Identified;

public class IdentifiedCommand<T, R>(
    Guid id,
    T command)
    : IRequest<R>
    where T : IRequest<R>
{
    public Guid Id { get; } = id;
    public T Command { get; } = command;
}
