using EnsureThat;
using Webshop.Order.Domain.Common;
using Webshop.Order.Domain.Entities;
using Webshop.Order.Domain.ValueObjects;

namespace Webshop.Order.Domain.AggregateRoots;

public sealed class PurchaseOrder : AggregateRoot
{
    public DateTime OrderDate { get; set; }

    public Guid BuyerId { get; set; }

    public Address Address { get; set; }

    public Discount Discount { get; set; }

    public List<OrderItem> OrderLines { get; set; }

    public PurchaseOrder(Guid buyerId, Address address, Discount discount)
    {
        Ensure.Guid.IsNotEmpty(buyerId, nameof(buyerId));

        BuyerId = buyerId;
        Address = address;
        Discount = discount;
        OrderLines = new List<OrderItem>();
    }

    public void AddItem(OrderItem item)
    {
        OrderLines.Add(item);
    }
}