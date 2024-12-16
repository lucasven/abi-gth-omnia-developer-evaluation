using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Products.GetProductCategories;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProductCategories;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings
{
    public class GetProductCategoriesProfile : Profile
    {
        public GetProductCategoriesProfile()
        {
            CreateMap<GetProductCategoriesResult, GetProductCategoriesResponse>();
        }
    }
}