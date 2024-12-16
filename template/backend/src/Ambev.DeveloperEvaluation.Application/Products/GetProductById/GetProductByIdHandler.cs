using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProductById
{
    public class GetProductByIdHandler : IRequestHandler<GetProductByIdCommand, GetProductByIdResult>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetProductByIdHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<GetProductByIdResult> Handle(GetProductByIdCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.Id);
            return product != null ? _mapper.Map<GetProductByIdResult>(product) : null;
        }
    }
} 