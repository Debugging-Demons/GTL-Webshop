using FluentValidation;
using Webshop.Order.Domain.Common;
using Webshop.Order.Domain.ValueObjects;

namespace Webshop.Order.Application.Features.Order.Request;

public record CreateOrderRequest(Guid BuyerId, Address Address, Discount Discount);

public class OrderValidator : AbstractValidator<CreateOrderRequest>
{
    public OrderValidator()
    {
        //name
        RuleFor(r => r.BuyerId)
            .NotEmpty().WithMessage(Errors.General.ValueIsEmpty(nameof(CreateOrderRequest.BuyerId)).Message);
    }
}
