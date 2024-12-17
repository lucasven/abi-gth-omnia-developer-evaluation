using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

public class CreateSaleResponse
{
    public Guid Id { get; set; }
    public int Number { get; set; }
    public SaleBranch Branch { get; set; }
    public DateTime CreatedAt { get; set; }
    public decimal TotalAmount { get; set; }
    public List<CreateSaleItemResponse> Items { get; set; } = new();
}
