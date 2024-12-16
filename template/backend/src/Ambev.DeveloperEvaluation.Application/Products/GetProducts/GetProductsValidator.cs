using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProducts
{
    public class GetProductsValidator : AbstractValidator<GetProductsCommand>
    {
        public GetProductsValidator()
        {
            RuleFor(x => x.Page)
                .GreaterThan(0)
                .WithMessage("Page must be greater than 0");

            RuleFor(x => x.Size)
                .GreaterThan(0)
                .LessThanOrEqualTo(100)
                .WithMessage("Size must be between 1 and 100");
        }
    }
} 