using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class SaleItem : BaseEntity
    { 
        public Sale Sale { get; set; }
        
        public Product Product { get; set; }
        
        public int Quantity { get; set; }

        public SaleItemStatus Status { get; set; }
        
        public decimal DiscountPercentage 
        { 
            get 
            {
                if (Quantity >= 4 && Quantity < 10)
                    return 10;
                else if (Quantity >= 10)
                    return 20;
                return 0;
            }
        }

        public decimal Total 
        { 
            get 
            {
                return (decimal)Quantity * Product.Price * (1 - DiscountPercentage/100);
            }
        }
    }
}
