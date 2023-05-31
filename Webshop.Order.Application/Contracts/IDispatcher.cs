
using Webshop.Order.Domain.Common;

namespace Webshop.Order.Application.Contracts;

public interface IDispatcher
{
    Task<Result<T>> Dispatch<T>(IQuery<T> query);
    Task<Result> Dispatch(ICommand command);
}
