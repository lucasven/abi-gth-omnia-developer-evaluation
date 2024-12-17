using AutoMapper;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSaleByNumber;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings
{
    public class GetSaleByNumberProfile : Profile
    {
        public GetSaleByNumberProfile()
        {
            CreateMap<Sale, GetSaleByNumberResponse>();
            CreateMap<Sale, GetSaleByNumberResponse>();
            CreateMap<User, GetSaleByNumberCustomer>();
            CreateMap<SaleItem, GetSaleByNumberSaleItem>();
        }
    }
}