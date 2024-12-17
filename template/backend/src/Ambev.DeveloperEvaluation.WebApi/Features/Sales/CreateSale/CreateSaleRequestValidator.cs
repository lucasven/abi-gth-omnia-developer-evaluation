using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

public class CreateSaleRequestValidator : AbstractValidator<CreateSaleRequest>
{
    public CreateSaleRequestValidator()
    {
        RuleFor(x => x.CustomerId)
            .NotEmpty().WithMessage("Customer ID is required");

        RuleFor(x => x.Branch)
            .IsInEnum().WithMessage("Invalid branch type");

        RuleFor(x => x.Items)
            .NotEmpty().WithMessage("At least one item is required")
            .ForEach(item =>
            {
                item.ChildRules(itemRules =>
                {
                    itemRules.RuleFor(x => x.ProductId)
                        .NotEmpty().WithMessage("Product ID is required");

                    itemRules.RuleFor(x => x.Quantity)
                        .GreaterThan(0).WithMessage("Quantity must be greater than 0")
                        .LessThan(21).WithMessage("Quantity must not be greater than 20");
                });
            });
    }
} 