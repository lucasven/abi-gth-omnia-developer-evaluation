using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<(IEnumerable<TEntity> Items, int TotalCount)> GetAllAsync(
            int page,
            int size,
            IQueryable<TEntity> query,
            string? orderBy = null);

        Task<TEntity?> GetByIdAsync(Guid id);
        Task<TEntity> CreateAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task DeleteAsync(Guid id);
    }
} 