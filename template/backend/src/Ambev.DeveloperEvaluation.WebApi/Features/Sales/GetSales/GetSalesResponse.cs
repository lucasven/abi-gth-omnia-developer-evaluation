using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.Common;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSales;

public class GetSalesResponse : BaseSale<CustomerResponse, SaleItemResponse>
{
}

public class CustomerResponse : BaseCustomer
{
}

public class SaleItemResponse : BaseSaleItem
{
}
