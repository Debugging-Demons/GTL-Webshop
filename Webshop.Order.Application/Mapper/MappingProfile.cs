using AutoMapper;
using AutoMapper.Execution;
using Webshop.Order.Application.Features.Order.Dtos;
using Webshop.Order.Domain.AggregateRoots;
using Webshop.Order.Domain.Entities;
using Webshop.Order.Domain.ValueObjects;

namespace Webshop.Order.Application.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<PurchaseOrder, PurchaseOrderDto>().ReverseMap();
        CreateMap<OrderItem, OrderItemDto>().ReverseMap();
        CreateMap<Address, AddressDto>().ReverseMap();
        CreateMap<Discount, int>().ConvertUsing<DiscountToInt>();
    }
}

public class DiscountToInt : ITypeConverter<Discount, int>
{
    public int Convert(Discount source, int destination, ResolutionContext _)
    {
        return source.Value;
    }
}
