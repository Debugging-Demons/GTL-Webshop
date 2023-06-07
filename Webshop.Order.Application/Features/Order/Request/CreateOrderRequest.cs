using FluentValidation;
using Webshop.Order.Application.Features.Order.Dtos;
using Webshop.Order.Domain.Common;

namespace Webshop.Order.Application.Features.Order.Request;

public record CreateOrderRequest(Guid BuyerId, AddressDto Address, DiscountDto Discount, List<OrderItemDto> OrderItems);

public class OrderValidator : AbstractValidator<CreateOrderRequest>
{
    public OrderValidator()
    {
        //name
        RuleFor(r => r.BuyerId)
            .NotEmpty().WithMessage(Errors.General.ValueIsEmpty(nameof(CreateOrderRequest.BuyerId)).Message);
        RuleFor(r => r.OrderItems)
            .NotEmpty().WithMessage(Errors.General.ValueIsEmpty(nameof(CreateOrderRequest.OrderItems)).Message);
    }
}
