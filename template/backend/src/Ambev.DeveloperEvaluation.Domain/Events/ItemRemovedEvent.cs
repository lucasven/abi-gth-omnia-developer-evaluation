using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Events;

public class ItemRemovedEvent
{
    public SaleItem Item { get; }
    public DateTime RemovedAt { get; }

    public ItemRemovedEvent(SaleItem item)
    {
        Item = item;
        RemovedAt = DateTime.UtcNow;
    }
} 