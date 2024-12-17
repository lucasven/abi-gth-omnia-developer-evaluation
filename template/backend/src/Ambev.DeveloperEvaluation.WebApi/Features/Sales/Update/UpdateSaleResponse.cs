using Ambev.DeveloperEvaluation.WebApi.Features.Sales.Common;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.Update;

public class UpdateSaleResponse : BaseSale<UpdateSaleCustomer, UpdateSaleItemResponse> { }

public class UpdateSaleCustomer : BaseCustomer { }

public class UpdateSaleItemResponse : BaseSaleItem { } 