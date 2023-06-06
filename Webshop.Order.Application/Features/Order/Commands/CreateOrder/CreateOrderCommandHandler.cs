using Microsoft.Extensions.Logging;
using Webshop.Order.Application.Contracts;
using Webshop.Order.Domain.AggregateRoots;
using Webshop.Order.Domain.Common;

namespace Webshop.Order.Application.Features.Order.Commands.CreateOrder;

public class CreateOrderCommandHandler : ICommandHandler<CreateOrderCommand, Guid>
{
    private ILogger<CreateOrderCommandHandler> _logger;
    private IOrderRepository _repository;
    public CreateOrderCommandHandler(ILogger<CreateOrderCommandHandler> logger, IOrderRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<Guid>> Handle(CreateOrderCommand command, CancellationToken cancellationToken = default)
    {
        Guid resId = await _repository.CreateAsync(command.Order);
        return Result.Ok(resId);
    }
}
