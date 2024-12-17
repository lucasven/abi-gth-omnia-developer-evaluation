using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Sale : BaseEntity
    {
        private readonly List<SaleItem> _saleItems;

        private Sale()
        {
            _saleItems = new List<SaleItem>();
            CreatedAt = DateTime.UtcNow;
            Status = SaleStatus.Created;
        }

        public int Number { get; private set; }
        public SaleBranch Branch { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }
        public User Customer { get; private set; }
        public decimal TotalAmount 
        { 
            get 
            { 
                return this._saleItems
                    //canceled items don't add to the total
                    .Where(si => si.Status != SaleItemStatus.Canceled)
                    .Select(si => (decimal)si.Quantity * si.Product.Price * (1 - si.DiscountPercentage/100))
                    .Sum(); 
            } 
        }
        public IReadOnlyCollection<SaleItem> SaleItems => _saleItems.AsReadOnly();
        public SaleStatus Status { get; private set; }

        public void AddItem(Product product, int quantity)
        {
            ValidateNewItem(product, quantity);

            var saleItem = new SaleItem() { Product = product, Quantity = quantity, Status = SaleItemStatus.Active };
            _saleItems.Add(saleItem);
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateItem(Guid id, int quantity, SaleItemStatus? saleItemStatus = null)
        {
            var sale = this._saleItems.FirstOrDefault(c => c.Id == id);
            if (sale == null)
                throw new DomainException("Item is not part of the sale items");
            if(quantity == 0 || quantity > 20)
                throw new DomainException("Invalid quantity.");

            sale.Quantity = quantity;
            if(saleItemStatus.HasValue)
                sale.Status = saleItemStatus.Value;
            UpdatedAt = DateTime.UtcNow;
        }

        public void RemoveItem(Guid itemId)
        {
            var item = _saleItems.FirstOrDefault(x => x.Id == itemId);
            if (item == null)
                throw new DomainException("Item not found in this sale.");

            if (Status != SaleStatus.Created)
                throw new DomainException("Cannot remove items from a sale that is not in Created status.");

            _saleItems.Remove(item);
            UpdatedAt = DateTime.UtcNow;
        }

        public void Cancel()
        {
            if (Status != SaleStatus.Created && Status != SaleStatus.Confirmed)
                throw new DomainException("Cannot cancel a sale that is not in Created or Confirmed status.");

            Status = SaleStatus.Cancelled;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Confirm()
        {
            if (Status != SaleStatus.Created)
                throw new DomainException("Cannot confirm a sale that is not in Created status.");

            if (!_saleItems.Any())
                throw new DomainException("Cannot confirm a sale without items.");

            Status = SaleStatus.Confirmed;
            UpdatedAt = DateTime.UtcNow;
        }

        private void ValidateNewItem(Product product, int quantity)
        {
            if (Status != SaleStatus.Created)
                throw new DomainException("Cannot add items to a sale that is not in Created status.");

            if (product == null)
                throw new DomainException("Product cannot be null.");

            if (quantity <= 0)
                throw new DomainException("Quantity must be greater than zero.");

            if (quantity > 20)
                throw new DomainException("Quantity must be twenty or less.");

            if (_saleItems.Any(x => x.Product.Id == product.Id))
                throw new DomainException("This product is already in the sale. Update the existing item instead.");
        }

        public static Sale Create(User customer, SaleBranch branch)
        {
            if (customer == null)
                throw new DomainException("Customer cannot be null.");
            if(branch == SaleBranch.None)
                throw new DomainException("Invalid sale branch.");

            return new Sale
            {
                Customer = customer,
                Branch = branch
            };
        }

        public ValidationResultDetail Validate()
        {
            var validator = new SaleValidator();
            var result = validator.Validate(this);
            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
            };
        }
    }
}
