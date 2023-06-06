using EnsureThat;
using MediatR;
using System.Diagnostics.CodeAnalysis;
using Webshop.Order.Application.Contracts;
using Webshop.Order.Domain.Common;

namespace Webshop.Order.Application;

[ExcludeFromCodeCoverage]
public class Dispatcher : IDispatcher
{
    public Dispatcher(IMediator mediator)
    {
        Ensure.That(mediator).IsNotNull();
        Mediator = mediator;
    }

    public IMediator Mediator { get; }

    public Task<Result<T>> Dispatch<T>(IQuery<T> query)
    {
        Ensure.That(query, nameof(query)).IsNotNull();
        return Mediator.Send(query);
    }

    public Task<Result<T>> Dispatch<T>(ICommand<T> command)
    {
        Ensure.That(command, nameof(command)).IsNotNull();
        return Mediator.Send(command);
    }
}
