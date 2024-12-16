using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.UpdateProduct
{
    public class UpdateProductRequestValidator : AbstractValidator<UpdateProductRequest>
    {
        public UpdateProductRequestValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.Price)
                .GreaterThan(0);

            RuleFor(x => x.Description)
                .NotEmpty();

            RuleFor(x => x.Category)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Image)
                .NotEmpty()
                .Must(uri => System.Uri.TryCreate(uri, System.UriKind.Absolute, out _))
                .WithMessage("Image must be a valid URI");

            When(x => x.Rating != null, () =>
            {
                RuleFor(x => x.Rating.RateValue)
                    .InclusiveBetween(0, 5)
                    .WithMessage("Rating rate must be between 0 and 5");

                RuleFor(x => x.Rating.Count)
                    .GreaterThanOrEqualTo(0)
                    .WithMessage("Rating count must be greater than or equal to 0");
            });
        }
    }
} 