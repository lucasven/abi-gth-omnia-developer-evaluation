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
        public ICollection<ProductRatingDTO> Ratings { get; set; }

        public ProductDTO()
        {
            Ratings = new List<ProductRatingDTO>();
        }
    }

    public class ProductRatingDTO
    {
        public int Id { get; set; }
        public decimal RateValue { get; set; }
        public int Count { get; set; }
    }

    public class ProductListResponseDTO
    {
        public IEnumerable<ProductDTO> Data { get; set; }
        public int TotalItems { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }

    public class AddProductRatingDTO
    {
        public decimal RateValue { get; set; }
    }
} 