using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

public class CreateSaleCommand : IRequest<CreateSaleResult>
{
    public SaleBranch Branch { get; set; }
    public Guid CustomerId { get; set; }
    public List<CreateSaleItemCommand> Items { get; set; } = new();
}
