using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Specifications;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

public class CreateSaleCommandHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IProductRepository _productRepository;
    private readonly IUserRepository _userRepository;
    private readonly IEventLogRepository _eventLogRepository;
    private readonly IMapper _mapper;

    public CreateSaleCommandHandler(
        ISaleRepository saleRepository,
        IProductRepository productRepository,
        IUserRepository userRepository,
        IEventLogRepository eventLogRepository,
        IMapper mapper)
    {
        _saleRepository = saleRepository;
        _productRepository = productRepository;
        _userRepository = userRepository;
        _eventLogRepository = eventLogRepository;
        _mapper = mapper;
    }

    public async Task<CreateSaleResult> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
    {
        // Validate customer exists
        var customer = await _userRepository.GetByIdAsync(request.CustomerId, cancellationToken);
        if (customer == null)
            throw new KeyNotFoundException($"Customer with id {request.CustomerId} not found");

        // Create sale using constructor
        var sale = Sale.Create(customer, request.Branch);

        // Process each item
        foreach (var itemRequest in request.Items)
        {
            var notMoreThan20specification = new NotMoreThan20EqualItemsSaleItemSpecification();
            var tier0specification = new DiscountTierZeroSaleItemSpecification();
            var tier1specification = new DiscountTierOneSaleItemSpecification();
            var tier2specification = new DiscountTierTwoSaleItemSpecification();
            var saleItem = new SaleItem { Quantity = itemRequest.Quantity };
            if (!notMoreThan20specification.IsSatisfiedBy(saleItem))
            {
                throw new InvalidOperationException("Item quantity cannot be more than 20");
            }
            if(!tier0specification.IsSatisfiedBy(saleItem))
            {
                throw new InvalidOperationException("Items with quantity smaller than 4 should not have a discount");
            }
            if (!tier1specification.IsSatisfiedBy(saleItem))
            {
                throw new InvalidOperationException("Items between 4 and 9 should have a 10% discount");
            }
            if (!tier2specification.IsSatisfiedBy(saleItem))
            {
                throw new InvalidOperationException("Items between 10 and 20 should have a 20% discount");
            }


            var product = await _productRepository.GetByIdAsync(itemRequest.ProductId);
            if (product == null)
                throw new KeyNotFoundException($"Product with id {itemRequest.ProductId} not found");

            // Add item using the proper method
            sale.AddItem(product, itemRequest.Quantity);
        }

        // Save the sale
        var createdSale = await _saleRepository.CreateAsync(sale);

        // Log the event
        await _eventLogRepository.LogEventAsync(new SaleCreatedEvent(createdSale), cancellationToken);

        // Map to result using AutoMapper
        return _mapper.Map<CreateSaleResult>(createdSale);
    }
} 