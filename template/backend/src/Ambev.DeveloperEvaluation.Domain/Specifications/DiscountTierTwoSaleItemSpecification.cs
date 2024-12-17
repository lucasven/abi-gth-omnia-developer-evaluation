using Ambev.DeveloperEvaluation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Specifications
{
    /// <summary>
    /// Tier 2 means 20% of discount is given for sales with more than 10 items of the same product
    /// </summary>
    public class DiscountTierTwoSaleItemSpecification : ISpecification<SaleItem>
    {
        public bool IsSatisfiedBy(SaleItem entity)
        {
            return entity.Quantity >= 10 && entity.Quantity <= 20 && entity.DiscountPercentage == 20m;
        }
    }
}
