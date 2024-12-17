using Ambev.DeveloperEvaluation.WebApi.Features.Sales.Common;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSaleByNumber;

public class GetSaleByNumberResponse : BaseSale<GetSaleByNumberCustomer, GetSaleByNumberSaleItem> { } 
public class GetSaleByNumberCustomer : BaseCustomer { }
public class GetSaleByNumberSaleItem: BaseSaleItem { }