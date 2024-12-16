using Ambev.DeveloperEvaluation.Domain.Common;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Product : BaseEntity
    {
        public Product()
        {
            Title = string.Empty;
            Description = string.Empty;
            Category = string.Empty;
            Image = string.Empty;
            Ratings = new List<ProductRating>();
        }

        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Image { get; set; }
        public ICollection<ProductRating> Ratings { get; set; }
    }

    public class ProductRating : BaseEntity
    {
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public decimal RateValue { get; set; }
        public int Count { get; set; }
    }
} 