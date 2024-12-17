using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.Domain.DTOs
{
    public class ProductDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Image { get; set; }
        public double? Rating { get; set; }
    }

    public class ProductRatingDTO
    {
        public Guid Id { get; set; }
        public decimal RateValue { get; set; }
        public int Count { get; set; }
    }
} 