using EnsureThat;
using Webshop.Order.Application.Contracts;
using Webshop.Order.Domain.AggregateRoots;

namespace Webshop.Order.Application.Features.Order.Commands.CreateOrder;

public sealed class CreateOrderCommand : ICommand<Guid>
{
    public PurchaseOrder Order { get; init; }

    public CreateOrderCommand(PurchaseOrder order)
    {
        Ensure.That(order.OrderItems, nameof(order.OrderItems)).HasItems();

        Order = order;
    }
}
