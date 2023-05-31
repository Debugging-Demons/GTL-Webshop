﻿using Microsoft.Extensions.Logging;
using Webshop.Order.Application.Contracts;
using Webshop.Order.Domain.AggregateRoots;
using Webshop.Order.Domain.Common;

namespace Webshop.Order.Application.Features.Order.Commands.CreateOrder;

public class CreateOrderCommandHandler : ICommandHandler<CreateOrderCommand>
{
    private ILogger<CreateOrderCommand> _logger;
    private IOrderRepository _repository;
    public CreateOrderCommandHandler(ILogger<CreateOrderCommand> logger, IOrderRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result> Handle(CreateOrderCommand command, CancellationToken cancellationToken = default)
    {
        PurchaseOrder newOrder = new PurchaseOrder(command.BuyerId, command.Address, command.Discount);
        await _repository.CreateAsync(newOrder);
        return Result.Ok();
    }
}
