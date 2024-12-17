using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Events;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

public class UpdateSaleCommandHandler : IRequestHandler<UpdateSaleCommand, Sale?>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IProductRepository _productRepository;
    private readonly IEventLogRepository _eventLogRepository;
    private readonly IMapper _mapper;

    public UpdateSaleCommandHandler(
        ISaleRepository saleRepository,
        IProductRepository productRepository,
        IEventLogRepository eventLogRepository,
        IMapper mapper)
    {
        _saleRepository = saleRepository;
        _productRepository = productRepository;
        _eventLogRepository = eventLogRepository;
        _mapper = mapper;
    }

    public async Task<Sale?> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
    {
        var sale = await _saleRepository.GetByIdAsync(request.Id);
        if (sale == null)
            throw new KeyNotFoundException("Sale not found");

        // Update sale status if requested
        if (request.Status.HasValue && sale.Status.ToString() != request.Status.Value.ToString())
        {
            switch (request.Status.Value)
            {
                case Domain.Enums.SaleStatus.Confirmed:
                    sale.Confirm();
                    await _eventLogRepository.LogEventAsync(new SaleModifiedEvent(sale, "StatusChange", "Sale confirmed"), cancellationToken);
                    break;
                case Domain.Enums.SaleStatus.Cancelled:
                    sale.Cancel();
                    await _eventLogRepository.LogEventAsync(new SaleCancelledEvent(sale), cancellationToken);
                    break;
                default:
                    throw new DomainException($"Cannot update sale to status {request.Status.Value}");
            }
        }

        // Handle items if provided
        if (request.Items != null)
        {
            var currentItemIds = sale.SaleItems.Select(si => si.Id).ToHashSet();
            var requestedItemIds = request.Items.Where(i => i.Id.HasValue).Select(i => i.Id.Value).ToHashSet();

            // Find items to remove
            var itemsToRemove = currentItemIds.Except(requestedItemIds);
            foreach (var itemId in itemsToRemove)
            {
                var itemToRemove = sale.SaleItems.First(si => si.Id == itemId);
                sale.RemoveItem(itemId);
                await _eventLogRepository.LogEventAsync(new ItemRemovedEvent(itemToRemove), cancellationToken);
            }

            // Process each requested item
            foreach (var item in request.Items)
            {
                if (item.Id.HasValue)
                {
                    var saleItem = sale.SaleItems.FirstOrDefault(si => si.Id == item.Id.Value);
                    if (saleItem == null)
                        throw new KeyNotFoundException($"Sale item with ID {item.Id.Value} not found");

                    if(item.Status.HasValue && item.Status.Value != saleItem.Status && item.Status == Domain.Enums.SaleItemStatus.Canceled)
                        await _eventLogRepository.LogEventAsync(new ItemCancelledEvent(saleItem), cancellationToken);

                    if (item.Status.HasValue && item.Status.Value != saleItem.Status || item.Quantity != saleItem.Quantity)
                    {
                        sale.UpdateItem(item.Id.Value, item.Quantity, item.Status);
                        await _eventLogRepository.LogEventAsync(new SaleModifiedEvent(sale, "ItemUpdate",
                            $"Updated item {item.Id.Value} quantity to {item.Quantity}"), cancellationToken);
                    }
                }
                else if (item.ProductId.HasValue)
                {
                    var product = await _productRepository.GetByIdAsync(item.ProductId.Value);
                    if (product == null)
                        throw new KeyNotFoundException($"Product with ID {item.ProductId.Value} not found");

                    var saleItem = sale.SaleItems.FirstOrDefault(si => si.Id == item.ProductId.Value);
                    if (saleItem != null)
                        throw new InvalidOperationException($"Sale item with ID {item.ProductId.Value} already exists on sale, increase quantity to add more");

                    sale.AddItem(product, item.Quantity);
                    await _eventLogRepository.LogEventAsync(new SaleModifiedEvent(sale, "ItemAdd",
                        $"Added new item for product {product.Id} with quantity {item.Quantity}"), cancellationToken);
                }
            }
        }

        await _saleRepository.UpdateAsync(sale);

        return await _saleRepository.GetByIdAsync(sale.Id);
    }
} 