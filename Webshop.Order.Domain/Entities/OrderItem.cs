using System.ComponentModel.DataAnnotations;
using Webshop.Order.Domain.Common;
using Webshop.Order.Domain.ValueObjects;

namespace Webshop.Order.Domain.Entities;

public sealed class OrderItem : Entity
{
    [Required(DisallowAllDefaultValues = true)]
    public Guid ProductId { get; set; }

    public Price Price { get; set; }

    public OrderItem(Guid productId, Price price)
    {
        ProductId = productId;
        Price = price;
    }
}
