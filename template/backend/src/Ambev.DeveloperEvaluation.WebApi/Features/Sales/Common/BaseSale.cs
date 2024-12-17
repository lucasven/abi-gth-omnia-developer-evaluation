using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.Common;

public class BaseSale<T, C> where T : BaseCustomer
    where C : BaseSaleItem
{
    public Guid Id { get; set; }
    public int Number { get; set; }
    public SaleBranch Branch { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public decimal TotalAmount { get; set; }
    public SaleStatus Status { get; set; }
    public T Customer { get; set; }
    public List<C> SaleItems { get; set; } = new();
}

public class BaseCustomer
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
}

public class BaseSaleItem
{
    public Guid Id { get; set; }
    public SaleItemStatus Status { get; set; }
    public int Quantity { get; set; }
    public decimal DiscountPercentage { get; set; }
    public decimal Total { get; set; }
} 