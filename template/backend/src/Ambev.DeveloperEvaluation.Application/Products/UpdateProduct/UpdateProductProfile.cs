using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.DTOs;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct
{
    public class UpdateProductProfile : Profile
    {
        public UpdateProductProfile()
        {
            CreateMap<UpdateProductCommand, Product>();
            CreateMap<Product, UpdateProductResult>();
            CreateMap<ProductRating, ProductRatingDTO>();
            CreateMap<ProductRatingDTO, ProductRating>();
        }
    }
} 