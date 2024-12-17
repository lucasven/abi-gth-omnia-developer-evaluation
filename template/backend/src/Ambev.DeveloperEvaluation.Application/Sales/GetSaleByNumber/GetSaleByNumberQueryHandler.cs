using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSaleByNumber;

public class GetSaleByNumberQueryHandler : IRequestHandler<GetSaleByNumberQuery, Sale?>
{
    private readonly ISaleRepository _saleRepository;

    public GetSaleByNumberQueryHandler(ISaleRepository saleRepository)
    {
        _saleRepository = saleRepository;
    }

    public async Task<Sale?> Handle(GetSaleByNumberQuery request, CancellationToken cancellationToken)
    {
        return await _saleRepository.GetByNumberAsync(request.Number, cancellationToken);
    }
} 