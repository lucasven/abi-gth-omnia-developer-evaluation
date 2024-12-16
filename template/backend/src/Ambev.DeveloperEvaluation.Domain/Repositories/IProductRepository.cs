using System.Collections.Generic;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<(IEnumerable<Product> Products, int TotalCount)> GetAllAsync(
            int page, 
            int size, 
            string orderBy,
            string? title = null,
            string? category = null,
            decimal? price = null,
            decimal? minPrice = null,
            decimal? maxPrice = null);
        Task<Product?> GetByIdAsync(Guid id);
        Task<Product> CreateAsync(Product product);
        Task<Product> UpdateAsync(Product product);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<string>> GetCategoriesAsync();
        Task<(IEnumerable<Product> Products, int TotalCount)> GetByCategoryAsync(string category, int page, int size, string orderBy);
    }
}