using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.DTOs;
using MediatR;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProductsByCategory
{
    public class GetProductsByCategoryHandler : IRequestHandler<GetProductsByCategoryCommand, GetProductsByCategoryResult>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetProductsByCategoryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<GetProductsByCategoryResult> Handle(GetProductsByCategoryCommand request, CancellationToken cancellationToken)
        {
            var (products, totalCount) = await _productRepository.GetByCategoryAsync(request.Category, request.Page, request.Size, request.Order);
            
            return new GetProductsByCategoryResult
            {
                Data = products.Select(p => _mapper.Map<ProductDTO>(p)),
                TotalItems = totalCount,
                CurrentPage = request.Page,
                TotalPages = (int)Math.Ceiling(totalCount / (double)request.Size)
            };
        }
    }
} 