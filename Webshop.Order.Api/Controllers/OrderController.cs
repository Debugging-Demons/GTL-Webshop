using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Webshop.Order.Application.Contracts;
using Webshop.Order.Application.Features.Order.Commands.CreateOrder;
using Webshop.Order.Application.Features.Order.Queries.GetOrder;
using Webshop.Order.Application.Features.Order.Request;
using Webshop.Order.Domain.AggregateRoots;

namespace Webshop.Order.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IDispatcher _dispatcher;
        private readonly ILogger<OrderController> _logger;
        private readonly IMapper _mapper;

        public OrderController(ILogger<OrderController> logger, IDispatcher dispatcher, IMapper mapper)
        {
            _dispatcher = dispatcher;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
        {
            // validate 
            OrderValidator validator = new();
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                _logger.LogError(string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)));
                return BadRequest(validationResult.Errors);
            }

            PurchaseOrder order = _mapper.Map<PurchaseOrder>(request);
            CreateOrderCommand command = new(order);
            var commandResult = await _dispatcher.Dispatch(command);

            return Ok(commandResult.Value);
        }

        [HttpGet]
        public async Task<IActionResult> GetOrder(Guid id)
        {
            GetOrderQuery query = new(id);
            var result = await _dispatcher.Dispatch(query);
            return result.Success ? Ok(result.Value) : BadRequest(result.Error);
        }
    }
}