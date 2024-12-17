using Ambev.DeveloperEvaluation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Events
{
    public class SaleModifiedEvent
    {
        public Sale Sale { get; }
        public DateTime ModifiedAt { get; }
        public string ModificationType { get; }
        public string Details { get; }

        public SaleModifiedEvent(Sale sale, string modificationType, string details)
        {
            Sale = sale;
            ModifiedAt = DateTime.UtcNow;
            ModificationType = modificationType;
            Details = details;
        }
    }
}
