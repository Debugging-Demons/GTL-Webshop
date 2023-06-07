using MediatR;
using Webshop.Order.Domain.Common;

namespace Webshop.Order.Application.Contracts;

public interface IQueryHandler<TQuery, TResult>
    : IRequestHandler<TQuery, Result<TResult>> where TQuery : IQuery<TResult>
{
    new Task<Result<TResult>> Handle(TQuery query, CancellationToken cancellationToken = default);
}
