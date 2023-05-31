using Webshop.Order.Domain.AggregateRoots;
using Webshop.Order.Domain.Common;
using Webshop.Order.Domain.Entities;
using Webshop.Order.Domain.ValueObjects;

namespace Webshop.Order.Domain.Test
{
    public class UnitTest1
    {
        [Theory]
        [InlineData(0)]
        [InlineData(15)]
        public void CreateDiscount_Valid_ExpectSuccess(int value)
        {
            // Act
            Discount sot = new Discount(value);

            //Assert
            Assert.Equal(value, sot.Value);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(16)]
        public void CreateDiscount_Invalid_ExpectException(int value)
        {
            // Arrange
            void act() => new Discount(value);

            //Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(act);
        }

        [Fact]
        public void CreateOrder_Valid_()
        {
            // Arrange
            Price price = new(10, Currency.DKK);
            OrderItem item = new(Guid.NewGuid(), price);
            Address address = new("vejvejsen", "Byborg", "Nordergion", "Land", "9000");
            Discount discount = new(0);

            PurchaseOrder sut = new(Guid.NewGuid(), Guid.NewGuid(), address, discount);

            // Act
            sut.AddItem(item);

            // Assert 
            Assert.Single(sut.OrderLines);
        }
    }
}