using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings
{
    public class CreateProductProfile : Profile
    {
        public CreateProductProfile()
        {
            CreateMap<CreateProductRequest, CreateProductCommand>();
            CreateMap<CreateProductResult, CreateProductResponse>();
        }
    }
}