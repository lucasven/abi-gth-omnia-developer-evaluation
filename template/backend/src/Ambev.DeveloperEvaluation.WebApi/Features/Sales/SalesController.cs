using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetSaleById;
using Ambev.DeveloperEvaluation.Application.Sales.GetSales;
using Ambev.DeveloperEvaluation.Application.Sales.GetSaleByNumber;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSales;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSaleByNumber;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.Update;
using Ambev.DeveloperEvaluation.WebApi.Sales.GetSaleById;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales;

[ApiController]
[Route("api/[controller]")]
public class SalesController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public SalesController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    /// <summary>
    /// Gets a list of sales with filtering capabilities
    /// </summary>
    /// <param name="request">The sales filter request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The filtered list of sales</returns>
    [HttpGet]
    [ProducesResponseType(typeof(PaginatedResponse<GetSalesResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetSales([FromQuery] GetSalesRequest request, CancellationToken cancellationToken)
    {
        var command = _mapper.Map<GetSalesCommand>(request);
        var result = await _mediator.Send(command, cancellationToken);

        return OkPaginated(new PaginatedList<GetSalesResponse>(
            _mapper.Map<List<GetSalesResponse>>(result.Data),
            result.TotalItems,
            request.Page,
            request.Size));
    }

    /// <summary>
    /// Creates a new sale
    /// </summary>
    /// <param name="request">The sale creation request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created sale details</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponseWithData<CreateSaleResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateSale([FromBody] CreateSaleRequest request, CancellationToken cancellationToken)
    {
        var validator = new CreateSaleRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<CreateSaleCommand>(request);
        var response = await _mediator.Send(command, cancellationToken);

        return Created(nameof(GetById), 
            new { id = response.Id }, 
            _mapper.Map<CreateSaleResponse>(response),
            "Sale created successfully");
    }

    /// <summary>
    /// Retrieve a Sale Using it's Guid
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponseWithData<GetSaleByIdResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var query = new GetSaleByIdQuery { Id = id };
        var response = await _mediator.Send(query);

        if (response == null)
            return NotFound();

        return Ok(_mapper.Map<GetSaleByIdResponse>(response), "Sale retrieved successfully");
    }

    /// <summary>
    /// Gets a sale by its number
    /// </summary>
    /// <param name="number">The sale number</param>
    /// <returns>The sale details</returns>
    [HttpGet("by-number/{number}")]
    [ProducesResponseType(typeof(ApiResponseWithData<GetSaleByNumberResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByNumber([FromRoute] int number)
    {
        var query = new GetSaleByNumberQuery { Number = number };
        var response = await _mediator.Send(query);

        if (response == null)
            return NotFound();

        return Ok(_mapper.Map<GetSaleByNumberResponse>(response), "Sale retrieved successfully");
    }

    /// <summary>
    /// Updates an existing sale
    /// </summary>
    /// <param name="id">The sale ID</param>
    /// <param name="request">The sale update request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated sale details</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponseWithData<UpdateSaleResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateSale([FromRoute] Guid id, [FromBody] UpdateSaleRequest request, CancellationToken cancellationToken)
    {
        var validator = new UpdateSaleRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<UpdateSaleCommand>(request);
        command.Id = id;
        var response = await _mediator.Send(command, cancellationToken);

        return Ok(_mapper.Map<UpdateSaleResponse>(response), "Sale updated successfully");
    }
} 