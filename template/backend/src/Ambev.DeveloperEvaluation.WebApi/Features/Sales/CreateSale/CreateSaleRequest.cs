using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

public class CreateSaleRequest
{
    public SaleBranch Branch { get; set; }
    public Guid CustomerId { get; set; }
    public List<CreateSaleItemRequest> Items { get; set; } = new();
}
