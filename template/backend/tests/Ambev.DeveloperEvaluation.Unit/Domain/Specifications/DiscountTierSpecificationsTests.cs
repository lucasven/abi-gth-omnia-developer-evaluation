using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Specifications;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Specifications
{
    public class DiscountTierSpecificationsTests
    {
        [Theory]
        [InlineData(0, true)]
        [InlineData(1, true)]
        [InlineData(3, true)]
        [InlineData(4, false)]   // Should be in tier 1
        public void TierZero_ShouldValidateQuantityAndDiscount(int quantity, bool expectedResult)
        {
            // Arrange
            var saleItem = new SaleItem { Quantity = quantity };
            var specification = new DiscountTierZeroSaleItemSpecification();

            // Act
            var result = specification.IsSatisfiedBy(saleItem);

            // Assert
            result.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData(4, true)]
        [InlineData(9, true)]
        [InlineData(3, false)]  // Too few items
        [InlineData(10, false)] // Should be in tier 2
        [InlineData(6, true)] 
        public void TierOne_ShouldValidateQuantityAndDiscount(int quantity, bool expectedResult)
        {
            // Arrange
            var saleItem = new SaleItem { Quantity = quantity };
            var specification = new DiscountTierOneSaleItemSpecification();

            // Act
            var result = specification.IsSatisfiedBy(saleItem);

            // Assert
            result.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData(10, true)]
        [InlineData(15, true)]
        [InlineData(20, true)]
        [InlineData(9, false)]   // Too few items
        [InlineData(21, false)]   // Too many items
        public void TierTwo_ShouldValidateQuantityAndDiscount(int quantity, bool expectedResult)
        {
            // Arrange
            var saleItem = new SaleItem { Quantity = quantity };
            var specification = new DiscountTierTwoSaleItemSpecification();

            // Act
            var result = specification.IsSatisfiedBy(saleItem);

            // Assert
            result.Should().Be(expectedResult);
        }
    }
} 