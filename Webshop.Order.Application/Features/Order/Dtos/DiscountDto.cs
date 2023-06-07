using System.Diagnostics.CodeAnalysis;
using Webshop.Order.Domain.ValueObjects;

namespace Webshop.Order.Application.Features.Order.Dtos;

[ExcludeFromCodeCoverage]
public sealed class DiscountDto
{
    public int Value { get; set; }

    public DiscountType Type { get; set; }
}