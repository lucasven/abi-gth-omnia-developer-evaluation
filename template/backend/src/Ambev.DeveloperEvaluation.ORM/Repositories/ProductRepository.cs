using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.ORM.Common;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<(IEnumerable<Product> Products, int TotalCount)> GetAllAsync(
            int page, 
            int size, 
            string? orderBy,
            string? title = null,
            string? category = null,
            decimal? price = null,
            decimal? minPrice = null,
            decimal? maxPrice = null)
        {
            var query = _dbSet.AsQueryable();

            if (!string.IsNullOrWhiteSpace(title))
            {
                if (title.EndsWith("*"))
                {
                    var searchTerm = title.TrimEnd('*');
                    query = query.Where(p => p.Title.ToLower().Contains(searchTerm.ToLower()));
                }
                else
                {
                    query = query.Where(p => p.Title.ToLower() == title.ToLower());
                }
            }

            if (!string.IsNullOrWhiteSpace(category))
            {
                query = query.Where(p => p.Category == category);
            }

            if (price.HasValue)
            {
                query = query.Where(p => p.Price == price.Value);
            }

            if (minPrice.HasValue)
            {
                query = query.Where(p => p.Price >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                query = query.Where(p => p.Price <= maxPrice.Value);
            }

            var result = await base.GetAllAsync(page, size, query, orderBy);
            return (result.Items, result.TotalCount);
        }

        protected override IQueryable<Product> ApplyDefaultOrder(IQueryable<Product> query)
        {
            return query.OrderBy(c => c.Title);
        }

        public async Task<IEnumerable<string>> GetCategoriesAsync()
        {
            return await _dbSet
                .Select(p => p.Category)
                .Distinct()
                .ToListAsync();
        }

        public async Task<(IEnumerable<Product> Products, int TotalCount)> GetByCategoryAsync(
            string category, 
            int page, 
            int size, 
            string orderBy)
        {
            var query = _dbSet.Where(p => p.Category == category);
            var result = await base.GetAllAsync(page, size, query, orderBy);
            return (result.Items, result.TotalCount);
        }
    }
} 