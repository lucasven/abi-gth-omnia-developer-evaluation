using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Sale : BaseEntity
    {
        public int Number { get; set; }

        public SaleBranch Branch { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public User Customer { get; set; }

        public decimal TotalAmount 
        { 
            get 
            { 
                return this.SaleItems
                    .Select(c => (decimal)c.Quantity * c.Product.Price * (1 - c.DiscountPercentage/100))
                    .Sum(); 
            } 
        }

        public IList<SaleItem> SaleItems { get; set; }

        public SaleStatus Status { get; set; }
    }
}
