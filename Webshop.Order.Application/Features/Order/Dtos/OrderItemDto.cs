using System.Diagnostics.CodeAnalysis;
using Webshop.Order.Domain.ValueObjects;

namespace Webshop.Order.Application.Features.Order.Dtos;

[ExcludeFromCodeCoverage]
public sealed record OrderItemDto(Guid ProductId, int Amount, Price Price);
