using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSales;

public class GetSalesResult
{
    public IEnumerable<Sale> Data { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int TotalItems { get; set; }

    public GetSalesResult(IEnumerable<Sale> data, int currentPage, int pageSize, int totalItems)
    {
        Data = data;
        CurrentPage = currentPage;
        TotalItems = totalItems;
        TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
    }
} 