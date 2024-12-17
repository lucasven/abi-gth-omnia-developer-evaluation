using Ambev.DeveloperEvaluation.Application.Sales.GetSaleById;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.WebApi.Sales.GetSaleById;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings
{
    public class GetSalesByIdProfile : Profile
    {
        public GetSalesByIdProfile()
        {
            CreateMap<Sale, GetSaleByIdResponse>();
            CreateMap<User, GetSaleByIdCustomerResponse>();
            CreateMap<SaleItem, GetSaleByIdSaleItemResponse>();
        }
    }
}
