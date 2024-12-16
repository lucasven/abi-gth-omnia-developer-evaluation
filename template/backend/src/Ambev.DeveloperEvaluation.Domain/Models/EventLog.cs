using System.Text.Json;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Ambev.DeveloperEvaluation.Domain.Models;

public class EventLog
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string EventType { get; set; }

    public string EventData { get; set; }

    public DateTime Timestamp { get; set; }

    public static EventLog FromEvent<T>(T eventData) where T : class
    {
        return new EventLog
        {
            EventType = typeof(T).Name,
            EventData = JsonSerializer.Serialize(eventData),
            Timestamp = DateTime.UtcNow
        };
    }
} 