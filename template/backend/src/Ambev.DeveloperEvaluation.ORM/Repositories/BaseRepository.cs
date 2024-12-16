using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        protected BaseRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public virtual async Task<(IEnumerable<TEntity> Items, int TotalCount)> GetAllAsync(
            int page,
            int size,
            IQueryable<TEntity> query,
            string? orderBy = null)
        {
            if (!string.IsNullOrWhiteSpace(orderBy))
            {
                try
                {
                    var orderByLower = orderBy.Trim().ToLower();
                    var validDirections = new[] { "asc", "desc" };
                    
                    var validProperties = typeof(TEntity)
                        .GetProperties()
                        .Select(p => p.Name.ToLower())
                        .ToArray();
                    
                    var parts = orderByLower.Split(' ');
                    if (parts.Length == 2 && 
                        validProperties.Contains(parts[0]) && 
                        validDirections.Contains(parts[1]))
                    {
                        var propertyName = char.ToUpper(parts[0][0]) + parts[0].Substring(1);
                        query = query.OrderBy($"{propertyName} {parts[1]}");
                    }
                    else
                    {
                        query = ApplyDefaultOrder(query);
                    }
                }
                catch
                {
                    query = ApplyDefaultOrder(query);
                }
            }
            else
            {
                query = ApplyDefaultOrder(query);
            }

            var totalCount = await query.CountAsync();
            var items = await query
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();

            return (items, totalCount);
        }

        protected virtual IQueryable<TEntity> ApplyDefaultOrder(IQueryable<TEntity> query)
        {
            return query.OrderBy(e => e.GetType().GetProperties().First().Name);
        }

        public virtual async Task<TEntity?> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task<TEntity> CreateAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task DeleteAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
} 