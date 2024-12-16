using Ambev.DeveloperEvaluation.Domain.Models;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

public interface IEventLogRepository
{
    Task LogEventAsync<T>(T eventData, CancellationToken cancellationToken = default) where T : class;
} 