using Ambev.DeveloperEvaluation.Domain.Enums;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSales;

public class GetSalesCommand : IRequest<GetSalesResult>
{
    public DateTime? CreatedAtMin { get; set; }
    public DateTime? CreatedAtMax { get; set; }
    public Guid? CustomerId { get; set; }
    public decimal? TotalAmountMin { get; set; }
    public decimal? TotalAmountMax { get; set; }
    public SaleBranch? Branch { get; set; }
    public SaleStatus? Status { get; set; }
    public int Page { get; set; }
    public int Size { get; set; }
    public string? OrderBy { get; set; }
} 