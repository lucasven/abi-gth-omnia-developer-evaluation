using Ambev.DeveloperEvaluation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Specifications
{
    /// <summary>
    /// Tier 0 means no ammount of discount is given for sales under 4 items of the same product
    /// </summary>
    public class DiscountTierZeroSaleItemSpecification : ISpecification<SaleItem>
    {
        public bool IsSatisfiedBy(SaleItem entity)
        {
            return entity.Quantity < 4 && entity.DiscountPercentage == 0;
        }
    }
}
