using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Products.GetProductsByCategory;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProductsByCategory;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings
{
    public class GetProductsByCategoryProfile : Profile
    {
        public GetProductsByCategoryProfile()
        {
            CreateMap<GetProductsByCategoryResult, GetProductsByCategoryResponse>();
        }
    }
}