using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.WebApi.Common;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSales;

public class GetSalesRequest : ApiPaginatedRequest
{
    public DateTime? CreatedAtMin { get; set; }
    public DateTime? CreatedAtMax { get; set; }
    public Guid? CustomerId { get; set; }
    public decimal? TotalAmountMin { get; set; }
    public decimal? TotalAmountMax { get; set; }
    public SaleBranch? Branch { get; set; }
    public SaleStatus? Status { get; set; }
} 