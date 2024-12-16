using Ambev.DeveloperEvaluation.Domain.DTOs;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct
{
    public class CreateProductResult
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Image { get; set; }
    }
} 