using Ambev.DeveloperEvaluation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Specifications
{
    /// <summary>
    /// Tier 1 means 10% of discount is given for sales with 4 or more and less than 10 items of the same product
    /// </summary>
    public class DiscountTierOneSaleItemSpecification : ISpecification<SaleItem>
    {
        public bool IsSatisfiedBy(SaleItem entity)
        {
            return entity.Quantity >= 4 && entity.Quantity < 10 && entity.DiscountPercentage == 10m;
        }
    }
}
