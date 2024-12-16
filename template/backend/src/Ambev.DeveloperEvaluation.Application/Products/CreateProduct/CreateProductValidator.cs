using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct
{
    public class CreateProductValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductValidator()
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

        }
    }
} 