using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

public interface ISaleRepository : IBaseRepository<Sale>
{
    Task<(IEnumerable<Sale> Sales, int TotalCount)> GetAllAsync(
        int page,
        int size,
        string? orderBy = null,
        Guid? customerId = null,
        DateTime? startDate = null,
        DateTime? endDate = null,
        decimal? totalAmountMin = null,
        decimal? totalAmountMax = null,
        SaleBranch? branch = null,
        SaleStatus? status = null);
    
    Task<Sale?> GetByNumberAsync(int number, CancellationToken cancellationToken);
} 