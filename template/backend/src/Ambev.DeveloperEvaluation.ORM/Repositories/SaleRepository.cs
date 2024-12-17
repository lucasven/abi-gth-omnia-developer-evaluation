using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class SaleRepository : BaseRepository<Sale>, ISaleRepository
{
    public SaleRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<(IEnumerable<Sale> Sales, int TotalCount)> GetAllAsync(
        int page,
        int size,
        string? orderBy = null,
        Guid? customerId = null,
        DateTime? startDate = null,
        DateTime? endDate = null,
        decimal? totalAmountMin = null,
        decimal? totalAmountMax = null,
        SaleBranch? branch = null,
        SaleStatus? status = null)
    {
        var query = _dbSet
            .Include(s => s.Customer)
            .Include(s => s.SaleItems)
                .ThenInclude(si => si.Product)
            .AsQueryable();

        if (customerId.HasValue)
        {
            query = query.Where(s => s.Customer.Id == customerId.Value);
        }

        if (startDate.HasValue)
        {
            query = query.Where(s => s.CreatedAt >= startDate.Value);
        }

        if (endDate.HasValue)
        {
            query = query.Where(s => s.CreatedAt <= endDate.Value);
        }

        if (totalAmountMin.HasValue)
        {
            query = query.Where(s => s.TotalAmount >= totalAmountMin.Value);
        }

        if (totalAmountMax.HasValue)
        {
            query = query.Where(s => s.TotalAmount <= totalAmountMax.Value);
        }

        if (branch.HasValue)
        {
            query = query.Where(s => s.Branch == branch.Value);
        }

        if (status.HasValue)
        {
            query = query.Where(s => s.Status == status.Value);
        }

        var result = await base.GetAllAsync(page, size, query, orderBy);
        return (result.Items, result.TotalCount);
    }

    protected override IQueryable<Sale> ApplyDefaultOrder(IQueryable<Sale> query)
    {
        return query.OrderByDescending(s => s.CreatedAt);
    }

    public override async Task<Sale?> GetByIdAsync(Guid id)
    {
        return await _dbSet
            .Include(s => s.Customer)
            .Include(s => s.SaleItems)
                .ThenInclude(si => si.Product)
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<Sale?> GetByNumberAsync(int number, CancellationToken cancellationToken)
    {
        return await _context.Sales
            .Include(s => s.Customer)
            .Include(s => s.SaleItems)
                .ThenInclude(si => si.Product)
            .FirstOrDefaultAsync(s => s.Number == number, cancellationToken);
    }
} 