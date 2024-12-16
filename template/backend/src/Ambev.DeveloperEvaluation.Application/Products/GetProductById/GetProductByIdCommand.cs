using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProductById
{
    public class GetProductByIdCommand : IRequest<GetProductByIdResult>
    {
        public Guid Id { get; set; }
    }
} 