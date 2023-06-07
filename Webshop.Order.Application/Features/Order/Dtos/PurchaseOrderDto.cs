using System.Diagnostics.CodeAnalysis;

namespace Webshop.Order.Application.Features.Order.Dtos;

[ExcludeFromCodeCoverage]
public sealed class PurchaseOrderDto
{
    public Guid Id { get; set; }

    public Guid BuyerId { get; set; }

    public AddressDto? Address { get; set; }

    public DiscountDto? Discount { get; set; }

    public List<OrderItemDto>? OrderItems { get; set; }
}
