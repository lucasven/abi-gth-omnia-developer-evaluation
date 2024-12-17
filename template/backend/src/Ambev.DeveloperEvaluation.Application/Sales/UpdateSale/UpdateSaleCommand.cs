using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

public class UpdateSaleCommand : IRequest<Sale?>
{
    public Guid Id { get; set; }
    public SaleStatus? Status { get; set; }
    public List<SaleItemCommand>? Items { get; set; }
}
