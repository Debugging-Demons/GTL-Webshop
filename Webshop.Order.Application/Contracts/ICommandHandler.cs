using MediatR;
using Webshop.Order.Domain.Common;

namespace Webshop.Order.Application.Contracts;

public interface ICommandHandler<TCommand>
   : IRequestHandler<TCommand, Result> where TCommand : ICommand
{
    new Task<Result> Handle(TCommand command, CancellationToken cancellationToken = default);
}
