using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Webshop.Order.Api.Controllers;
using Webshop.Order.Application.Contracts;
using Webshop.Order.Application.Features.Order.Commands.CreateOrder;
using Webshop.Order.Application.Features.Order.Dtos;
using Webshop.Order.Application.Features.Order.Queries.GetOrder;
using Webshop.Order.Application.Mapper;
using Webshop.Order.Domain.AggregateRoots;
using Webshop.Order.Domain.Common;
using Webshop.Order.Domain.Entities;
using Webshop.Order.Domain.ValueObjects;
using Webshop.Order.Persistence;

namespace Webshop.Order.Domain.Test;

public class OrderUnitTests
{
    [Theory]
    [InlineData(0)]
    [InlineData(int.MaxValue)]
    public void Create_Price_Valid_Expect_Success(int value)
    {
        // Act
        Price sut = new(value, Currency.DKK);

        //Assert
        Assert.Equal(value, sut.Value);
    }

    [Theory]
    [InlineData(-1)]
    public void Create_Price_InValid_Expect_Failure(int value)
    {
        // Act
        void act() => new Price(value, Currency.DKK);

        //Assert
        Assert.Throws<ArgumentOutOfRangeException>(act);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(15)]
    public void CreateDiscount_Valid_ExpectSuccess(int value)
    {
        // Act
        Discount sut = new(value, DiscountType.Percent);

        //Assert
        Assert.Equal(value, sut.Value);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(16)]
    public void CreateDiscount_Invalid_ExpectException(int value)
    {
        // Arrange
        void act() => new Discount(value, DiscountType.Percent);

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(act);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(int.MaxValue)]
    public void CreateOrderItem_Valid_ExpectSuccess(int value)
    {
        // Arrange
        Price price = new(10, Currency.DKK);

        // Act
        OrderItem item = new(Guid.NewGuid(), value, price);

        //Assert
        Assert.Equal(value, item.Amount);
    }

    [Theory]
    [InlineData(0)]
    public void CreateOrderItem_InValid_ExpectFailure(int value)
    {
        // Arrange
        Price price = new(10, Currency.DKK);
        void act() => new OrderItem(Guid.NewGuid(), value, price);

        //Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(act);
    }

    [Theory]
    [InlineData(int.MaxValue)]
    public void CreateOrderItem_InValid_Max_ExpectFailure(int value)
    {
        // Arrange
        Price price = new(10, Currency.DKK);
        void act() => new OrderItem(Guid.NewGuid(), ++value, price);

        //Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(act);
    }

    [Fact]
    public void CreateOrderCommand_Invalid_ExpectFailure()
    {
        // Arrange
        Address address = new("vejvejsen", "Byborg", "Nordregion", "Land", "9000");
        Discount discount = new(0, DiscountType.Percent);
        PurchaseOrder order = new(Guid.NewGuid(), address, discount);

        // Act
        void act() => new CreateOrderCommand(order);

        // Assert
        Assert.Throws<ArgumentException>(act);
    }

    [Fact]
    public async Task CreateOrderHandler_Invoke_RepositoryCreate_ExpectSuccessSpy()
    {
        // Arrange
        var logger = new Mock<ILogger<CreateOrderCommandHandler>>();
        var repository = new Mock<IOrderRepository>();

        Address address = new("vejvejsen", "Byborg", "Nordregion", "Land", "9000");
        Discount discount = new(0, DiscountType.Percent);
        PurchaseOrder order = new(Guid.NewGuid(), address, discount);
        Price price = new(10, Currency.DKK);
        OrderItem item = new(Guid.NewGuid(), 1, price);
        order.AddItem(item);

        CreateOrderCommand command = new(order);
        CreateOrderCommandHandler handler = new(logger.Object, repository.Object);

        // Act
        Result result = await handler.Handle(command);

        // Assert
        repository.Verify((r => r.CreateAsync(order)), Times.Once);
    }

    [Fact]
    public async Task CreateOrderHandler_Invoke_RepositoryCreate_ExpectSameGuid()
    {
        // Arrange
        var logger = new Mock<ILogger<CreateOrderCommandHandler>>();
        var repository = new Mock<IOrderRepository>();

        Address address = new("vejvejsen", "Byborg", "Nordregion", "Land", "9000");
        Discount discount = new(0, DiscountType.Percent);
        PurchaseOrder order = new(Guid.NewGuid(), address, discount);
        Price price = new(10, Currency.DKK);
        OrderItem item = new(Guid.NewGuid(), 1, price);
        order.AddItem(item);

        CreateOrderCommand command = new(order);
        CreateOrderCommandHandler handler = new(logger.Object, repository.Object);

        Guid newGuid = Guid.Parse("a5fe47f3-86d6-4637-ac58-c647142ccbe9");
        repository.Setup(r => r.CreateAsync(order)).Returns(Task.FromResult(newGuid));

        // Act
        Result<Guid> result = await handler.Handle(command);

        // Assert
        Assert.Equal(newGuid, result.Value);
    }

    [Fact]
    public async void GetOrderHandler_Invoke_Repository_Read_Expect_Success_Spy()
    {
        // Arrange
        var logger = new Mock<ILogger<GetOrderQueryHandler>>();
        var repository = new Mock<IOrderRepository>();
        var mapper = new Mock<IMapper>();

        Guid newGuid = Guid.NewGuid();
        GetOrderQuery command = new(newGuid);
        GetOrderQueryHandler handler = new(logger.Object, repository.Object, mapper.Object);

        // Act
        Result result = await handler.Handle(command);

        // Assert
        repository.Verify((r => r.GetById(newGuid)), Times.Once);
    }

    [Fact]
    public async void GetOrderHandler_Invoke_Repository_Read_Expect_Success()
    {
        // Arrange
        var logger = new Mock<ILogger<GetOrderQueryHandler>>();
        var repository = new Mock<IOrderRepository>();
        var mapper = new Mock<IMapper>();

        /// create order to "search" for
        Address address = new("vejvejsen", "Byborg", "Nordregion", "Land", "9000");
        Discount discount = new(0, DiscountType.Percent);
        PurchaseOrder order = new(Guid.NewGuid(), address, discount);
        Guid newGuid = Guid.NewGuid();
        order.Id = newGuid;

        PurchaseOrderDto orderDto = new()
        {
            Id = newGuid,
            BuyerId = Guid.NewGuid(),
            Address = null,
            Discount = null,
            OrderItems = null
        };

        GetOrderQuery command = new(newGuid);
        GetOrderQueryHandler handler = new(logger.Object, repository.Object, mapper.Object);

        repository.Setup(r => r.GetById(newGuid)).Returns(Task.FromResult(order));
        mapper.Setup(m => m.Map<PurchaseOrderDto>(order)).Returns(orderDto);

        // Act
        Result result = await handler.Handle(command);

        // Assert
        Assert.True(result.Success);
    }

    [Fact]
    public async void GetOrderHandler_Invoke_Repository_Read_Expect_Failure()
    {
        // Arrange
        var logger = new Mock<ILogger<GetOrderQueryHandler>>();
        var repository = new Mock<IOrderRepository>();
        var mapper = new Mock<IMapper>();

        /// create order to "search" for
        Address address = new("vejvejsen", "Byborg", "Nordregion", "Land", "9000");
        Discount discount = new(0, DiscountType.Percent);
        PurchaseOrder order = new(Guid.NewGuid(), address, discount);
        Guid newGuid = Guid.NewGuid();
        order.Id = newGuid;

        PurchaseOrderDto orderDto = new()
        {
            Id = newGuid,
            BuyerId = Guid.NewGuid(),
            Address = null,
            Discount = null,
            OrderItems = null
        };

        GetOrderQuery command = new(Guid.NewGuid());
        GetOrderQueryHandler handler = new(logger.Object, repository.Object, mapper.Object);

        mapper.Setup(m => m.Map<PurchaseOrderDto>(order)).Returns(orderDto);

        // Act
        Result result = await handler.Handle(command);

        // Assert
        Assert.False(result.Success);
    }

    [Fact]
    public async void Create_Order_Flow_Expect_Success_Id()
    {
        // Arrange
        var logger = new Mock<ILogger<CreateOrderCommandHandler>>();
        var container = new Container<PurchaseOrder>();
        var repository = new OrderRepository(container, Mock.Of<IDataContext>());
        var mapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MappingProfile());
        }).CreateMapper();

        /// create order to "search" for
        Address address = new("vejvejsen", "Byborg", "Nordregion", "Land", "9000");
        Discount discount = new(0, DiscountType.Percent);
        PurchaseOrder order = new(Guid.NewGuid(), address, discount);
        Price price = new(10, Currency.DKK);
        OrderItem item = new(Guid.NewGuid(), 1, price);
        order.AddItem(item);

        CreateOrderCommand command = new(order);
        CreateOrderCommandHandler handler = new(logger.Object, repository);

        // Act
        Result<Guid> result = await handler.Handle(command);

        // Assert
        Assert.Equal(container.Items.First().Id, result.Value);
    }
}