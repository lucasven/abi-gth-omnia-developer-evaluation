using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;

/// <summary>
/// Provides methods for generating test data for Sale entities using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class SaleTestData
{
    /// <summary>
    /// Configures the Faker to generate valid Sale entities.
    /// The generated sales will have valid:
    /// - Number
    /// - Branch
    /// - Customer
    /// - Status
    /// - Creation date
    /// </summary>
    private static readonly Faker<Sale> SaleFaker = new Faker<Sale>()
        .CustomInstantiator(f => Sale.Create(
            UserTestData.GenerateValidUser(),
            SaleBranch.B2B
        ));

    /// <summary>
    /// Generates a valid Sale entity with randomized data.
    /// The generated sale will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Sale entity with randomly generated data.</returns>
    public static Sale GenerateValidSale()
    {
        return SaleFaker.Generate();
    }

    /// <summary>
    /// Generates a valid Product for testing sale items.
    /// </summary>
    /// <returns>A valid Product with random data.</returns>
    public static Product GenerateValidProduct()
    {
        return new Faker<Product>()
            .RuleFor(p => p.Id, f => Guid.NewGuid())
            .RuleFor(p => p.Title, f => f.Commerce.ProductName())
            .RuleFor(p => p.Description, f => f.Commerce.ProductDescription())
            .RuleFor(p => p.Image, f => f.Internet.Url())
            .RuleFor(p => p.Category, f => f.Commerce.Department())
            .RuleFor(p => p.Price, f => decimal.Parse(f.Commerce.Price(1, 1000, 2)))
            .Generate();
    }

    /// <summary>
    /// Generates a valid quantity for a sale item.
    /// </summary>
    /// <returns>A valid quantity between 1 and 100.</returns>
    public static int GenerateValidQuantity()
    {
        return new Faker().Random.Number(1, 20);
    }

    /// <summary>
    /// Generates an invalid quantity (zero or negative).
    /// </summary>
    /// <returns>An invalid quantity.</returns>
    public static int GenerateInvalidQuantity()
    {
        return new Faker().Random.Number(21, 1000);
    }

    /// <summary>
    /// Generates a valid SaleItem for testing.
    /// </summary>
    /// <returns>A valid SaleItem with random data.</returns>
    public static SaleItem GenerateValidSaleItem()
    {
        var faker = new Faker();
        return new SaleItem
        {
            Product = GenerateValidProduct(),
            Quantity = GenerateValidQuantity(),
            Status = SaleItemStatus.Active
        };
    }
} 