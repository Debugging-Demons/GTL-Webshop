namespace Webshop.Order.Application.Features.Order.Dtos;

public record PurchaseOrderDto(Guid Id, DateTime LastModified, DateTime Created, Guid BuyerId, AddressDto Address, int Discount, List<OrderItemDto> OrderItems);
