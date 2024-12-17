using Ambev.DeveloperEvaluation.Domain.Common;

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
        public decimal? Rating
        {
            get
            {
                return Ratings != null && Ratings.Count > 0 
                    ? Ratings.Select(r => r.RateValue * (decimal)r.Count).Sum() / Ratings.Select(c => c.Count).Sum()
                    : null;
            }
        }
    }
} 