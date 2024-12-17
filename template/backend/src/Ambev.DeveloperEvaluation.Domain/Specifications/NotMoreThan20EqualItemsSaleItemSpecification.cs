using Ambev.DeveloperEvaluation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Specifications
{
    /// <summary>
    /// Sales should not have more than 20 sale items per product
    /// </summary>
    public class NotMoreThan20EqualItemsSaleItemSpecification : ISpecification<SaleItem>
    {
        public bool IsSatisfiedBy(SaleItem entity)
        {
            return entity.Quantity <= 20;
        }
    }
}
