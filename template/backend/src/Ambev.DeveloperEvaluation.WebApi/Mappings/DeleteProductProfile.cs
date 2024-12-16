using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.DeleteProduct;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings
{
    public class DeleteProductProfile : Profile
    {
        public DeleteProductProfile()
        {
            CreateMap<DeleteProductRequest, DeleteProductCommand>();
        }
    }
}