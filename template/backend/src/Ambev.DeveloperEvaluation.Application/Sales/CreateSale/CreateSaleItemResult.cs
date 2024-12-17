namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

public class CreateSaleItemResult
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal DiscountPercentage { get; set; }
    public decimal Total { get; set; }
}
