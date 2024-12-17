using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Application.Products.GetProducts;
using Ambev.DeveloperEvaluation.Application.Products.GetProductById;
using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;
using Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;
using Ambev.DeveloperEvaluation.Application.Products.GetProductCategories;
using Ambev.DeveloperEvaluation.Application.Products.GetProductsByCategory;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProducts;
using AutoMapper;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProductById;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.GetUser;
using System.Threading;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProductsByCategory;
using Ambev.DeveloperEvaluation.Domain.DTOs;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper mapper;

        public ProductsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(PaginatedResponse<GetProductsResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll([FromQuery] GetProductsRequest request)
        {
            var command = mapper.Map<GetProductsCommand>(request);
            var result = await _mediator.Send(command);
            return Ok(mapper.Map<PaginatedResponse<GetProductsResponse>>(result));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponseWithData<GetProductByIdResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var request = new GetProductByIdRequest { Id = id };

            var validator = new GetProductByIdRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);


            var command = mapper.Map<GetProductByIdCommand>(request);
            var result = await _mediator.Send(command);

            if (result == null)
                return NotFound();

            return Ok(mapper.Map<GetProductByIdResponse>(result), "Product retrieved successfully");
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponseWithData<CreateProductResult>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateProduct.CreateProductRequest request)
        {
            var validator = new CreateProductRequestValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = mapper.Map<CreateProductCommand>(request);

            var response = await _mediator.Send(command);
            return Created(nameof(GetById), new {id = response.Id}, mapper.Map<CreateProductResult>(response),"Product created successfully");
        }

        [HttpPut]
        [ProducesResponseType(typeof(ApiResponseWithData<UpdateProductResult>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromBody] UpdateProduct.UpdateProductRequest request)
        {
            var command = mapper.Map<UpdateProductCommand>(request);

            var result = await _mediator.Send(command);

            if (result == null)
                return NotFound();

            return Ok(mapper.Map<UpdateProductResult>(result), "Product updated successfully");
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponseWithData<DeleteProductResult>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var command = new DeleteProductCommand { Id = id };
            var result = await _mediator.Send(command);

            if (!result.Success)
                return NotFound(result);

            return Ok(mapper.Map<DeleteProductResult>(result), "Product deleted successfully");
        }

        [HttpGet("categories")]
        [ProducesResponseType(typeof(ApiResponseWithData<IEnumerable<string>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCategories()
        {
            var command = new GetProductCategoriesCommand();
            var result = await _mediator.Send(command);
            return Ok(mapper.Map<GetProductCategoriesResult>(result).Categories, "Product categories retrieved successfully");
        }

        [HttpGet("category/{Category}")]
        [ProducesResponseType(typeof(ApiResponseWithData<ProductDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByCategory([FromQuery] GetProductsByCategoryRequest request)
        {
            var command = mapper.Map<GetProductsByCategoryCommand>(request);

            var result = await _mediator.Send(command);
            return OkPaginated(new PaginatedList<ProductDTO>(mapper.Map<List<ProductDTO>>(result.Data),
                result.TotalItems,
                request.Page,
                request.Size));
        }
    }
}