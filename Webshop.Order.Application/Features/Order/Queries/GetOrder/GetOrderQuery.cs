using Webshop.Order.Application.Contracts;
using Webshop.Order.Application.Features.Order.Dtos;

namespace Webshop.Order.Application.Features.Order.Queries.GetOrder;

public record GetOrderQuery(Guid Id) : IQuery<PurchaseOrderDto>;
