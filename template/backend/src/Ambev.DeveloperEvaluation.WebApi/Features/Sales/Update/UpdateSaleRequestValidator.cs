using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.Update;

public class UpdateSaleRequestValidator : AbstractValidator<UpdateSaleRequest>
{
    public UpdateSaleRequestValidator()
    {
        RuleFor(x => x)
            .Must(x => x.Status.HasValue || x.Items?.Any() == true)
            .WithMessage("At least one update operation must be specified");

        When(x => x.Items != null, () =>
        {
            RuleForEach(x => x.Items)
                .SetValidator(new SaleItemRequestValidator());
        });
    }
}
