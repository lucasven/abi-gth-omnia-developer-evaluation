using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.Update;

public class SaleItemRequestValidator : AbstractValidator<SaleItemRequest>
{
    public SaleItemRequestValidator()
    {
        RuleFor(x => x)
            .Must(x => x.Id.HasValue || x.ProductId.HasValue)
            .WithMessage("Either Id or ProductId must be specified");

        When(x => !x.Id.HasValue, () =>
        {
            RuleFor(x => x.ProductId)
                .NotEmpty()
                .WithMessage("ProductId is required for new items");
        });

        RuleFor(x => x.Quantity)
            .GreaterThan(0)
            .LessThanOrEqualTo(20)
            .WithMessage("Quantity must be between 1 and 20");
    }
} 