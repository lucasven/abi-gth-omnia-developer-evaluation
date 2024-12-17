using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.Update;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings;

public class UpdateSaleMappingProfile : Profile
{
    public UpdateSaleMappingProfile()
    {
        CreateMap<UpdateSaleRequest, UpdateSaleCommand>();
        CreateMap<SaleItemRequest, SaleItemCommand>();

        CreateMap<Sale, UpdateSaleResponse>();
        CreateMap<SaleItem, UpdateSaleItemResponse>();
        CreateMap<User, UpdateSaleCustomer>();
    }
}