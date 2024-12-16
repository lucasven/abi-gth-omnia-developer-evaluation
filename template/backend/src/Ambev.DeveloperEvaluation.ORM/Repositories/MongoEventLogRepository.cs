using Ambev.DeveloperEvaluation.Domain.Models;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MongoDB.Driver;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class MongoEventLogRepository : IEventLogRepository
{
    private readonly IMongoCollection<EventLog> _collection;

    public MongoEventLogRepository(IMongoDatabase database)
    {
        _collection = database.GetCollection<EventLog>("event_logs");
    }

    public async Task LogEventAsync<T>(T eventData, CancellationToken cancellationToken = default) where T : class
    {
        var eventLog = EventLog.FromEvent(eventData);
        await _collection.InsertOneAsync(eventLog, null, cancellationToken);
    }
} 