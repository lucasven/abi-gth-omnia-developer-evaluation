using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation
{
    public class SaleValidator : AbstractValidator<Sale>
    {
        public SaleValidator()
        {
            RuleFor(sale => sale.Customer.Id)
                .NotEmpty()
                .WithMessage("Customer ID is required");

            RuleFor(sale => sale.Branch)
                .IsInEnum()
                .WithMessage("Invalid branch type");

            RuleFor(sale => sale.SaleItems)
                .NotEmpty()
                .WithMessage("At least one item is required");

            RuleForEach(sale => sale.SaleItems)
                .ChildRules(item =>
                {
                    // Basic item validation
                    item.RuleFor(x => x.Product.Id)
                        .NotEmpty()
                        .WithMessage("Product ID is required");

                    item.RuleFor(x => x.Quantity)
                        .GreaterThan(0)
                        .WithMessage("Quantity must be greater than 0")
                        .LessThanOrEqualTo(20)
                        .WithMessage("Quantity must not exceed 20 items");

                    // Discount Tier validation rules
                    item.RuleFor(x => x)
                        .Must((saleItem) => 
                        {
                            if (saleItem.Quantity < 4)
                                return saleItem.DiscountPercentage == 0;
                            if (saleItem.Quantity >= 4 && saleItem.Quantity < 10)
                                return saleItem.DiscountPercentage == 10m;
                            if (saleItem.Quantity >= 10)
                                return saleItem.DiscountPercentage == 20m;
                            return false;
                        })
                        .WithMessage(x => 
                            $"Invalid discount percentage for quantity {x.Quantity}. " +
                            $"Expected: {(x.Quantity < 4 ? "0%" : x.Quantity < 10 ? "10%" : "20%")}, " +
                            $"Actual: {x.DiscountPercentage}%");
                });

            RuleFor(sale => sale.TotalAmount)
                .GreaterThan(0)
                .WithMessage("Total amount must be greater than 0");

            RuleFor(sale => sale.CreatedAt)
                .NotEmpty()
                .WithMessage("Sale date is required")
                .LessThanOrEqualTo(DateTime.UtcNow)
                .WithMessage("Sale date cannot be in the future");
        }
    }
}
