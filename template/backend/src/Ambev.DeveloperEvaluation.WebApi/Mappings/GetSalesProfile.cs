using Ambev.DeveloperEvaluation.Application.Sales.GetSales;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSales;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings;

public class GetSalesProfile : Profile
{
    public GetSalesProfile()
    {
        CreateMap<GetSalesRequest, GetSalesCommand>();

        CreateMap<Sale, GetSalesResponse>();
        CreateMap<User, CustomerResponse>();
        CreateMap<SaleItem, SaleItemResponse>();
    }
} 