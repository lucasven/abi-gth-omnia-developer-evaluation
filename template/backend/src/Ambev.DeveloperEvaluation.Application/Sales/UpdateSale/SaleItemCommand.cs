using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

public class SaleItemCommand
{
    public Guid? Id { get; set; }
    public Guid? ProductId { get; set; }
    public int Quantity { get; set; }
    public SaleItemStatus? Status { get; set; }
}