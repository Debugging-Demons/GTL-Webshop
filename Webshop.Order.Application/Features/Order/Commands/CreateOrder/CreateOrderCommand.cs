using Webshop.Order.Application.Contracts;
using Webshop.Order.Domain.ValueObjects;

namespace Webshop.Order.Application.Features.Order.Commands.CreateOrder;

public record CreateOrderCommand(Guid BuyerId, Address Address, Discount Discount) : ICommand<Guid>;
