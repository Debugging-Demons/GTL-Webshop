using MediatR;
using Webshop.Order.Domain.Common;

namespace Webshop.Order.Application.Contracts;

public interface IQuery<T> : IRequest<Result<T>> { }
