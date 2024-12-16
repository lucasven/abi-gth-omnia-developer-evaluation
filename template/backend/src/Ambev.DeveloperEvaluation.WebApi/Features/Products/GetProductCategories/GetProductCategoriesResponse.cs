using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProductCategories
{
    public class GetProductCategoriesResponse
    {
        public IEnumerable<string> Categories { get; set; }
    }
} 