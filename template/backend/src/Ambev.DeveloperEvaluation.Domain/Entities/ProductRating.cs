using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class ProductRating : BaseEntity
    {
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public decimal RateValue { get; set; }
        public int Count { get; set; }
    }
} 