using System.Collections.Generic;
using Ambev.DeveloperEvaluation.Domain.DTOs;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProducts
{
    public class GetProductsResult
    {
        public IEnumerable<ProductDTO> Data { get; set; }
        public int TotalItems { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
} 