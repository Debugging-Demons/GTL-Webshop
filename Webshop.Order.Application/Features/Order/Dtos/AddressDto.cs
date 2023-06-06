using System.Diagnostics.CodeAnalysis;

namespace Webshop.Order.Application.Features.Order.Dtos;

[ExcludeFromCodeCoverage]
public sealed record AddressDto(string Street, string City, string State, string Country, string ZipCode);