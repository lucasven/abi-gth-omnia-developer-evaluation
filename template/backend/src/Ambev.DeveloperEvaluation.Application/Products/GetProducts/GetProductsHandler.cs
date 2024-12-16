using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.DTOs;
using MediatR;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProducts
{
    public class GetProductsHandler : IRequestHandler<GetProductsCommand, GetProductsResult>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetProductsHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<GetProductsResult> Handle(GetProductsCommand request, CancellationToken cancellationToken)
        {
            var (products, totalCount) = await _productRepository.GetAllAsync(
                request.Page, 
                request.Size, 
                request.Order,
                request.Title,
                request.Category,
                request.Price,
                request.MinPrice,
                request.MaxPrice);
            
            return new GetProductsResult
            {
                Data = products.Select(p => _mapper.Map<ProductDTO>(p)),
                TotalItems = totalCount,
                CurrentPage = request.Page,
                TotalPages = (int)Math.Ceiling(totalCount / (double)request.Size)
            };
        }
    }
} 