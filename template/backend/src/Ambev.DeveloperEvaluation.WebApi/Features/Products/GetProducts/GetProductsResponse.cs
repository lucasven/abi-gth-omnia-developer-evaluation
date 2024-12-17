using System.Collections.Generic;
using Ambev.DeveloperEvaluation.Domain.DTOs;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProducts
{
    public class GetProductsResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Image { get; set; }
        public decimal? Rating { get; set; }
    }
} 