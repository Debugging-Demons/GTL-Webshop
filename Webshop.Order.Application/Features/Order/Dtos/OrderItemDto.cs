namespace Webshop.Order.Application.Features.Order.Dtos;

public record OrderItemDto(Guid ProductId, int Price, string Currency);
