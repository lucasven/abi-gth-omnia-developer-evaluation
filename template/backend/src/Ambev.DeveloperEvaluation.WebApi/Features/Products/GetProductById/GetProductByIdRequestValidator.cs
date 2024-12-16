using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProductById
{
    public class GetProductByIdRequestValidator : AbstractValidator<GetProductByIdRequest>
    {
        public GetProductByIdRequestValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Product Id must have a value");
        }
    }
} 