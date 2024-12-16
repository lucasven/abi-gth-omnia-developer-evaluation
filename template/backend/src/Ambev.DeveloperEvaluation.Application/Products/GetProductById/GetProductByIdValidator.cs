using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProductById
{
    public class GetProductByIdValidator : AbstractValidator<GetProductByIdCommand>
    {
        public GetProductByIdValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Product Id must have a value");
        }
    }
} 