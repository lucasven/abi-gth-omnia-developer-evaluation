using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Products.GetProducts;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.Domain.DTOs;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProducts;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings
{
    public class GetProductsProfile : Profile
    {
        public GetProductsProfile()
        {
            CreateMap<ProductDTO, GetProductsResponse>();
            CreateMap<GetProductsResult, PaginatedResponse<GetProductsResponse>>();
            CreateMap<GetProductsRequest, GetProductsCommand>();
        }
    }
}