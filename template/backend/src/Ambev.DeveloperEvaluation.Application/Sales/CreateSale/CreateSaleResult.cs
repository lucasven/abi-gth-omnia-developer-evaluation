using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

public class CreateSaleResult
{
    public Guid Id { get; set; }
    public int Number { get; set; }
    public SaleBranch Branch { get; set; }
    public DateTime CreatedAt { get; set; }
    public decimal TotalAmount { get; set; }
    public List<CreateSaleItemResult> Items { get; set; } = new();
}
