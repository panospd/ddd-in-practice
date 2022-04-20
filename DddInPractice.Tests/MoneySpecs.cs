using System;
using DddInPractice.Logic;
using FluentAssertions;
using Xunit;

namespace DddInPractice.Tests
{
    public class MoneySpecs
    {
        [Fact]
        public void Sum_of_two_moneys_produces_correct_result()
        {
            // Arrange
            var money1 = new Money(1, 2, 3, 4, 5, 6);
            var money2 = new Money(1, 2, 3, 4, 5, 6);

            // Act
            var sum = money1 + money2;

            // Assert
            sum.OneCentCount.Should().Be(2);
            sum.TenCentCount.Should().Be(4);
            sum.QuarterCount.Should().Be(6);
            sum.OneDollarCount.Should().Be(8);
            sum.FiveDollarCount.Should().Be(10);
            sum.TwentyDollarCount.Should().Be(12);
        }

        [Fact]
        public void Two_moneys_instances_equal_if_contain_the_same_money_amount()
        {
            // Arrange
            // Act
            var money1 = new Money(1, 2, 3, 4, 5, 6);
            var money2 = new Money(1, 2, 3, 4, 5, 6);

            // Assert
            money1.Should().Be(money2);
        }

        [Fact]
        public void Two_moneys_instances_do_not_equal_if_contain_the_different_money_amount()
        {
            // Arrange
            // Act
            var dollar = new Money(0, 0, 0, 1, 0, 0);
            var hundredCents = new Money(100, 0, 0, 0, 0, 0);

            // Assert
            dollar.Should().NotBe(hundredCents);
        }

        [Theory]
        [InlineData(-1, 0, 0, 0, 0, 0)]
        [InlineData(0, -2, 0, 0, 0, 0)]
        [InlineData(0, 0, -3, 0, 0, 0)]
        [InlineData(0, 0, 0, -4, 0, 0)]
        [InlineData(0, 0, 0, 0, -5, 0)]
        [InlineData(0, 0, 0, 0, 0, -6)]
        public void cannot_create_money_with_negative_value(
            int oneCentCount,
            int tenCentCount,
            int quarterCount,
            int oneDollarCount,
            int fiveDollarCount,
            int twentyDollarCount)
        {
            // Arrange
            // Act
            Action action = () => new Money(
                oneCentCount,
                tenCentCount,
                quarterCount,
                oneDollarCount,
                fiveDollarCount,
                twentyDollarCount);

            // Assert
            action.Should().Throw<InvalidOperationException>();
        }
        
        [Theory]
        [InlineData(0, 0, 0, 0, 0, 0, 0)]
        [InlineData(1, 0, 0, 0, 0, 0, 0.01)]
        [InlineData(1, 2, 0, 0, 0, 0, 0.21)]
        [InlineData(1, 2, 3, 0, 0, 0, 0.96)]
        [InlineData(1, 2, 3, 4, 0, 0, 4.96)]
        [InlineData(1, 2, 3, 4, 5, 0, 29.96)]
        [InlineData(1, 2, 3, 4, 5, 6, 149.96)]
        [InlineData(11, 0, 0, 0, 0, 0, 0.11)]
        [InlineData(110, 0, 0, 0, 100, 0,501.1)]
        public void Ammount_is_calculated_correctly(
            int oneCentCount,
            int tenCentCount,
            int quarterCount,
            int oneDollarCount,
            int fiveDollarCount,
            int twentyDollarCount,
            decimal expectedAmount
        )
        {
            // Arrange
            var money = new Money(
                oneCentCount,
                tenCentCount,
                quarterCount,
                oneDollarCount,
                fiveDollarCount,
                twentyDollarCount);

            // Act
            // Assert
            money.Amount.Should().Be(expectedAmount);
        }

        [Fact]
        public void Subtraction_of_two_moneys_produces_correct_result()
        {
            // Arrange
            var money1 = new Money(10, 10, 10, 10, 10, 10);
            var money2 = new Money(1, 2, 3, 4, 5, 6);

            // Act
            var sum = money1 - money2;

            // Assert
            sum.OneCentCount.Should().Be(9);
            sum.TenCentCount.Should().Be(8);
            sum.QuarterCount.Should().Be(7);
            sum.OneDollarCount.Should().Be(6);
            sum.FiveDollarCount.Should().Be(5);
            sum.TwentyDollarCount.Should().Be(4);
        }

        [Fact]
        public void Cannot_subtract_more_than_exists()
        {
            // Arrange
            var money1 = new Money(0, 1, 0, 0, 0, 0);
            var money2 = new Money(1, 0, 0, 0, 0, 0);

            // Act
            Action action = () => {
                var subtraction = money1 - money2;
            };

            // Assert
            action.Should().Throw<InvalidOperationException>();
        }
    }
}
