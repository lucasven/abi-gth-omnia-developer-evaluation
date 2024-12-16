using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Products.GetProductById;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProductById;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings
{
    public class GetProductByIdProfile : Profile
    {
        public GetProductByIdProfile()
        {
            CreateMap<GetProductByIdResult, GetProductByIdResponse>();
            CreateMap<GetProductByIdRequest, GetProductByIdCommand>();
        }
    }
}