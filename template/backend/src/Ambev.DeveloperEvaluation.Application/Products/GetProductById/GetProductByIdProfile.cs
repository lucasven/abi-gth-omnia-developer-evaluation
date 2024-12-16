using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.DTOs;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProductById
{
    public class GetProductByIdProfile : Profile
    {
        public GetProductByIdProfile()
        {
            CreateMap<Product, GetProductByIdResult>();
            CreateMap<ProductRating, ProductRatingDTO>();
        }
    }
} 