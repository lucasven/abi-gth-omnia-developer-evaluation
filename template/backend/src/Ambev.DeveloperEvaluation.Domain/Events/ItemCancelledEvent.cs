using Ambev.DeveloperEvaluation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Events
{
    public class ItemCancelledEvent
    {
        public SaleItem Item { get; }
        public DateTime CancelledAt { get; }

        public ItemCancelledEvent(SaleItem item)
        {
            Item = item;
            CancelledAt = DateTime.UtcNow;
        }
    }
}
