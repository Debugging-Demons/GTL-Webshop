using MediatR;
using Webshop.Order.Domain.Common;

namespace Webshop.Order.Application.Contracts;

public interface ICommandHandler<TCommand, TResult>
   : IRequestHandler<TCommand, Result<TResult>> where TCommand : ICommand<TResult>
{
    new Task<Result<TResult>> Handle(TCommand command, CancellationToken cancellationToken = default);
}
