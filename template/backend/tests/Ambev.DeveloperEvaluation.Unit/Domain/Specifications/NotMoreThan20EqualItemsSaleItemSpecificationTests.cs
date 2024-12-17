using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Specifications;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Specifications
{
    public class NotMoreThan20EqualItemsSaleItemSpecificationTests
    {
        [Theory]
        [InlineData(1, true)]
        [InlineData(20, true)]
        [InlineData(21, false)]
        [InlineData(50, false)]
        public void IsSatisfiedBy_ShouldValidateQuantity(int quantity, bool expectedResult)
        {
            // Arrange
            var saleItem = new SaleItem { Quantity = quantity };
            var specification = new NotMoreThan20EqualItemsSaleItemSpecification();

            // Act
            var result = specification.IsSatisfiedBy(saleItem);

            // Assert
            result.Should().Be(expectedResult);
        }
    }
} 