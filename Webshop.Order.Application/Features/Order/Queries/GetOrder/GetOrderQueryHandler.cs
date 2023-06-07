using AutoMapper;
using Microsoft.Extensions.Logging;
using Webshop.Order.Application.Contracts;
using Webshop.Order.Application.Features.Order.Commands.CreateOrder;
using Webshop.Order.Application.Features.Order.Dtos;
using Webshop.Order.Domain.Common;

namespace Webshop.Order.Application.Features.Order.Queries.GetOrder;

public class GetOrderQueryHandler : IQueryHandler<GetOrderQuery, PurchaseOrderDto>
{
    private ILogger<GetOrderQueryHandler> _logger;
    private IOrderRepository _repository;
    private IMapper _mapper;

    public GetOrderQueryHandler(ILogger<GetOrderQueryHandler> logger, IOrderRepository repository, IMapper mapper)
    {
        _logger = logger;
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<PurchaseOrderDto>> Handle(GetOrderQuery query, CancellationToken cancellationToken = default)
    {
        var order = await _repository.GetById(query.Id);
        if (order is null)
        {
            _logger.Log(LogLevel.Error, $"No order found for id: {query.Id}");
            return Result.Fail<PurchaseOrderDto>(Errors.General.NotFound(query.Id));
        }
        return _mapper.Map<PurchaseOrderDto>(order);
    }
}