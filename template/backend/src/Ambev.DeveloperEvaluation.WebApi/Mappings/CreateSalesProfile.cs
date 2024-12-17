using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings;

public class CreateSalesProfile : Profile
{
    public CreateSalesProfile()
    {
        CreateMap<CreateSaleRequest, CreateSaleCommand>();
        CreateMap<CreateSaleItemRequest, CreateSaleItemCommand>();

        CreateMap<CreateSaleResult, CreateSaleResponse>();
        CreateMap<CreateSaleItemResult, CreateSaleItemResponse>();
    }
}