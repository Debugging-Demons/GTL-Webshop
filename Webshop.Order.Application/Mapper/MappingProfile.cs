using AutoMapper;
using System.Diagnostics.CodeAnalysis;
using Webshop.Order.Application.Features.Order.Dtos;
using Webshop.Order.Application.Features.Order.Request;
using Webshop.Order.Domain.AggregateRoots;
using Webshop.Order.Domain.Entities;
using Webshop.Order.Domain.ValueObjects;

namespace Webshop.Order.Application.Mapper;

[ExcludeFromCodeCoverage]
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<PurchaseOrder, PurchaseOrderDto>().ReverseMap();
        CreateMap<PurchaseOrder, CreateOrderRequest>().ReverseMap();
        CreateMap<OrderItem, OrderItemDto>().ReverseMap();
        CreateMap<Address, AddressDto>().ReverseMap();
        CreateMap<Discount, DiscountDto>().ReverseMap();
    }
}
