using Ambev.DeveloperEvaluation.WebApi.Common;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProducts
{
    public class GetProductsRequest : ApiPaginatedRequest
    {
        [FromQuery]
        public string? Title { get; set; }

        [FromQuery]
        public string? Category { get; set; }

        [FromQuery]
        public decimal? Price { get; set; }

        [FromQuery(Name="_minPrice")]
        public decimal? MinPrice { get; set; }

        [FromQuery(Name ="_maxPrice")]
        public decimal? MaxPrice { get; set; }
    }
} 