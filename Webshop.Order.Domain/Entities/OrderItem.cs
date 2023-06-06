using EnsureThat;
using System.ComponentModel.DataAnnotations;
using Webshop.Order.Domain.Common;
using Webshop.Order.Domain.ValueObjects;

namespace Webshop.Order.Domain.Entities;

public sealed class OrderItem : Entity
{
    [Required(DisallowAllDefaultValues = true)]
    public Guid ProductId { get; set; }

    public int Amount { get; set; }

    public Price Price { get; set; }

    public OrderItem(Guid productId, int amount, Price price)
    {
        Ensure.That(amount, nameof(amount)).IsGt(0);

        ProductId = productId;
        Amount = amount;
        Price = price;
    }
}
