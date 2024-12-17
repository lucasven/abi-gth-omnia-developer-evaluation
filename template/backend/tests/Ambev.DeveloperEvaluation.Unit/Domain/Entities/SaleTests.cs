using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

/// <summary>
/// Contains unit tests for the Sale entity class.
/// Tests cover status changes, item management, and validation scenarios.
/// </summary>
public class SaleTests
{
    /// <summary>
    /// Tests that a new sale is created with the correct initial state.
    /// </summary>
    [Fact(DisplayName = "New sale should be created with correct initial state")]
    public void Given_NewSale_When_Created_Then_ShouldHaveCorrectInitialState()
    {
        // Arrange
        var customer = UserTestData.GenerateValidUser();
        var branch = SaleBranch.B2B;
        
        // Act
        var sale = Sale.Create(customer, branch);

        // Assert
        sale.Customer.Should().Be(customer);
        sale.Branch.Should().Be(branch);
        sale.Status.Should().Be(SaleStatus.Created);
        sale.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        sale.SaleItems.Should().BeEmpty();
    }

    /// <summary>
    /// Tests that adding a valid item to a sale works correctly.
    /// </summary>
    [Fact(DisplayName = "Adding valid item should succeed")]
    public void Given_ValidSaleItem_When_AddingToSale_Then_ShouldSucceed()
    {
        // Arrange
        var sale = SaleTestData.GenerateValidSale();
        var product = SaleTestData.GenerateValidProduct();
        var quantity = SaleTestData.GenerateValidQuantity();

        // Act
        sale.AddItem(product, quantity);

        // Assert
        sale.SaleItems.Should().HaveCount(1);
        var addedItem = sale.SaleItems.First();
        addedItem.Product.Should().Be(product);
        addedItem.Quantity.Should().Be(quantity);
        addedItem.Status.Should().Be(SaleItemStatus.Active);
        sale.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }

    /// <summary>
    /// Tests that adding an item with invalid quantity fails.
    /// </summary>
    [Fact(DisplayName = "Adding item with invalid quantity should fail")]
    public void Given_InvalidQuantity_When_AddingItem_Then_ShouldThrowException()
    {
        // Arrange
        var sale = SaleTestData.GenerateValidSale();
        var product = SaleTestData.GenerateValidProduct();
        var invalidQuantity = SaleTestData.GenerateInvalidQuantity();
        
        // Act & Assert
        var action = () => sale.AddItem(product, invalidQuantity);
        action.Should().Throw<DomainException>()
            .WithMessage("Quantity must be twenty or less.");
    }

    /// <summary>
    /// Tests that adding a duplicate product fails.
    /// </summary>
    [Fact(DisplayName = "Adding duplicate product should fail")]
    public void Given_DuplicateProduct_When_AddingItem_Then_ShouldThrowException()
    {
        // Arrange
        var sale = SaleTestData.GenerateValidSale();
        var product = SaleTestData.GenerateValidProduct();
        sale.AddItem(product, SaleTestData.GenerateValidQuantity());

        // Act & Assert
        var action = () => sale.AddItem(product, SaleTestData.GenerateValidQuantity());
        action.Should().Throw<DomainException>()
            .WithMessage("This product is already in the sale. Update the existing item instead.");
    }

    /// <summary>
    /// Tests that confirming a sale with items succeeds.
    /// </summary>
    [Fact(DisplayName = "Confirming sale with items should succeed")]
    public void Given_SaleWithItems_When_Confirmed_Then_ShouldSucceed()
    {
        // Arrange
        var sale = SaleTestData.GenerateValidSale();
        sale.AddItem(SaleTestData.GenerateValidProduct(), SaleTestData.GenerateValidQuantity());

        // Act
        sale.Confirm();

        // Assert
        sale.Status.Should().Be(SaleStatus.Confirmed);
        sale.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }

    /// <summary>
    /// Tests that confirming a sale without items fails.
    /// </summary>
    [Fact(DisplayName = "Confirming sale without items should fail")]
    public void Given_SaleWithoutItems_When_Confirmed_Then_ShouldThrowException()
    {
        // Arrange
        var sale = SaleTestData.GenerateValidSale();

        // Act & Assert
        var action = () => sale.Confirm();
        action.Should().Throw<DomainException>()
            .WithMessage("Cannot confirm a sale without items.");
    }

    /// <summary>
    /// Tests that cancelling a sale in valid status succeeds.
    /// </summary>
    [Theory(DisplayName = "Cancelling sale in valid status should succeed")]
    [InlineData(SaleStatus.Created)]
    [InlineData(SaleStatus.Confirmed)]
    public void Given_SaleInValidStatus_When_Cancelled_Then_ShouldSucceed(SaleStatus initialStatus)
    {
        // Arrange
        var sale = SaleTestData.GenerateValidSale();
        if (initialStatus == SaleStatus.Confirmed)
        {
            sale.AddItem(SaleTestData.GenerateValidProduct(), SaleTestData.GenerateValidQuantity());
            sale.Confirm();
        }

        // Act
        sale.Cancel();

        // Assert
        sale.Status.Should().Be(SaleStatus.Cancelled);
        sale.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }

    /// <summary>
    /// Tests that total amount is calculated correctly with discounts.
    /// </summary>
    [Fact(DisplayName = "Total amount should be calculated correctly with discounts")]
    public void Given_SaleWithItems_When_CalculatingTotal_Then_ShouldBeCorrect()
    {
        // Arrange
        var sale = SaleTestData.GenerateValidSale();
        var product1 = SaleTestData.GenerateValidProduct();
        var product2 = SaleTestData.GenerateValidProduct();
        
        var quantity1 = 4;
        var discount1 = 10m; // 10%
        var quantity2 = 11;
        var discount2 = 20m; // 20%

        sale.AddItem(product1, quantity1);
        sale.AddItem(product2, quantity2);

        // Calculate expected total
        var item1Total = quantity1 * product1.Price * (1 - discount1/100);
        var item2Total = quantity2 * product2.Price * (1 - discount2/100);
        var expectedTotal = item1Total + item2Total;

        // Act & Assert
        sale.TotalAmount.Should().Be(expectedTotal);
    }

    /// <summary>
    /// Tests that adding items to a non-Created sale fails.
    /// </summary>
    [Theory(DisplayName = "Adding items to non-Created sale should fail")]
    [InlineData(SaleStatus.Confirmed)]
    [InlineData(SaleStatus.Cancelled)]
    public void Given_NonCreatedSale_When_AddingItem_Then_ShouldThrowException(SaleStatus status)
    {
        // Arrange
        var sale = SaleTestData.GenerateValidSale();
        if (status == SaleStatus.Confirmed)
        {
            sale.AddItem(SaleTestData.GenerateValidProduct(), SaleTestData.GenerateValidQuantity());
            sale.Confirm();
        }
        else if (status == SaleStatus.Cancelled)
        {
            sale.Cancel();
        }

        // Act & Assert
        var action = () => sale.AddItem(
            SaleTestData.GenerateValidProduct(),
            SaleTestData.GenerateValidQuantity());

        action.Should().Throw<DomainException>()
            .WithMessage("Cannot add items to a sale that is not in Created status.");
    }

    /// <summary>
    /// Tests that removing items from a non-Created sale fails.
    /// </summary>
    [Theory(DisplayName = "Removing items from non-Created sale should fail")]
    [InlineData(SaleStatus.Confirmed)]
    [InlineData(SaleStatus.Cancelled)]
    public void Given_NonCreatedSale_When_RemovingItem_Then_ShouldThrowException(SaleStatus status)
    {
        // Arrange
        var sale = SaleTestData.GenerateValidSale();
        var product = SaleTestData.GenerateValidProduct();
        sale.AddItem(product, SaleTestData.GenerateValidQuantity());
        var itemId = sale.SaleItems.First().Id;

        if (status == SaleStatus.Confirmed)
        {
            sale.Confirm();
        }
        else if (status == SaleStatus.Cancelled)
        {
            sale.Cancel();
        }

        // Act & Assert
        var action = () => sale.RemoveItem(itemId);
        action.Should().Throw<DomainException>()
            .WithMessage("Cannot remove items from a sale that is not in Created status.");
    }

    /// <summary>
    /// Tests that creating a sale with null customer fails.
    /// </summary>
    [Fact(DisplayName = "Creating sale with null customer should fail")]
    public void Given_NullCustomer_When_CreatingSale_Then_ShouldThrowException()
    {
        // Arrange
        User nullCustomer = null;
        var branch = SaleBranch.B2B;

        // Act & Assert
        var action = () => Sale.Create(nullCustomer, branch);
        action.Should().Throw<DomainException>()
            .WithMessage("Customer cannot be null.");
    }

    /// <summary>
    /// Tests that creating a sale with invalid branch fails.
    /// </summary>
    [Fact(DisplayName = "Creating sale with invalid branch should fail")]
    public void Given_InvalidBranch_When_CreatingSale_Then_ShouldThrowException()
    {
        // Arrange
        var customer = UserTestData.GenerateValidUser();
        var invalidBranch = SaleBranch.None; // Invalid enum value

        // Act & Assert
        var action = () => Sale.Create(customer, invalidBranch);
        action.Should().Throw<DomainException>()
            .WithMessage("Invalid sale branch.");
    }

    /// <summary>
    /// Tests that updating quantity to invalid value fails.
    /// </summary>
    [Fact(DisplayName = "Updating quantity to invalid value should fail")]
    public void Given_InvalidQuantity_When_UpdatingItem_Then_ShouldThrowException()
    {
        // Arrange
        var sale = SaleTestData.GenerateValidSale();
        var product = SaleTestData.GenerateValidProduct();
        sale.AddItem(product, SaleTestData.GenerateValidQuantity());
        var itemId = sale.SaleItems.First().Id;
        var invalidQuantity = 0;

        // Act & Assert
        var action = () => sale.UpdateItem(itemId, invalidQuantity);
        action.Should().Throw<DomainException>()
            .WithMessage("Invalid quantity.");
    }

    /// <summary>
    /// Tests that updating non-existent item fails.
    /// </summary>
    [Fact(DisplayName = "Updating non-existent item should fail")]
    public void Given_NonExistentItem_When_UpdatingQuantity_Then_ShouldThrowException()
    {
        // Arrange
        var sale = SaleTestData.GenerateValidSale();
        var nonExistentItemId = Guid.NewGuid();
        var quantity = SaleTestData.GenerateValidQuantity();

        // Act & Assert
        var action = () => sale.UpdateItem(nonExistentItemId, quantity);
        action.Should().Throw<DomainException>()
            .WithMessage("Item is not part of the sale items");
    }

    /// <summary>
    /// Tests that removing non-existent item fails.
    /// </summary>
    [Fact(DisplayName = "Removing non-existent item should fail")]
    public void Given_NonExistentItem_When_RemovingItem_Then_ShouldThrowException()
    {
        // Arrange
        var sale = SaleTestData.GenerateValidSale();
        var nonExistentItemId = Guid.NewGuid();

        // Act & Assert
        var action = () => sale.RemoveItem(nonExistentItemId);
        action.Should().Throw<DomainException>()
            .WithMessage("Item not found in this sale.");
    }

    /// <summary>
    /// Tests that adding item with null product fails.
    /// </summary>
    [Fact(DisplayName = "Adding item with null product should fail")]
    public void Given_NullProduct_When_AddingItem_Then_ShouldThrowException()
    {
        // Arrange
        var sale = SaleTestData.GenerateValidSale();
        Product nullProduct = null;
        var quantity = SaleTestData.GenerateValidQuantity();

        // Act & Assert
        var action = () => sale.AddItem(nullProduct, quantity);
        action.Should().Throw<DomainException>()
            .WithMessage("Product cannot be null.");
    }
} 