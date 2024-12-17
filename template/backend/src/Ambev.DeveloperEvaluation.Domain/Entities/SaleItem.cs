using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Enums;
using System.Text.Json.Serialization;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class SaleItem : BaseEntity
    {
        [JsonIgnore]
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
                //total of each saleitem canceled should be 0
                if(Status == SaleItemStatus.Canceled) return 0;
                return (decimal)Quantity * Product.Price * (1 - DiscountPercentage/100);
            }
        }
    }
}
