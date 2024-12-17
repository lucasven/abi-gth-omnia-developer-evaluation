using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSaleByNumber;

public class GetSaleByNumberQuery : IRequest<Domain.Entities.Sale?>
{
    public int Number { get; set; }
} 