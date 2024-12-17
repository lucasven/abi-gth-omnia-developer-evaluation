using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.Update;

public class UpdateSaleRequest
{
    public SaleStatus? Status { get; set; }
    public List<SaleItemRequest>? Items { get; set; }
}
