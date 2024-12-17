using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.Update;

public class SaleItemRequest
{
    public Guid? Id { get; set; }
    public Guid? ProductId { get; set; }
    public int Quantity { get; set; }
    public SaleItemStatus? Status { get; set; }
} 