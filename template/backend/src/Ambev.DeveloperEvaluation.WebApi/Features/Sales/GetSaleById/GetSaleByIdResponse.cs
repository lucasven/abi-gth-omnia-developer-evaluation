using Ambev.DeveloperEvaluation.WebApi.Features.Sales.Common;

namespace Ambev.DeveloperEvaluation.WebApi.Sales.GetSaleById
{
    public class GetSaleByIdResponse : BaseSale<GetSaleByIdCustomerResponse, GetSaleByIdSaleItemResponse>
    {
    }

    public class GetSaleByIdCustomerResponse : BaseCustomer
    {
    }

    public class GetSaleByIdSaleItemResponse : BaseSaleItem
    {
    }
} 