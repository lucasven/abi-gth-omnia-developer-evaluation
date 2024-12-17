using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSales;

public class GetSalesCommandHandler : IRequestHandler<GetSalesCommand, GetSalesResult>
{
    private readonly ISaleRepository _saleRepository;

    public GetSalesCommandHandler(ISaleRepository saleRepository)
    {
        _saleRepository = saleRepository;
    }

    public async Task<GetSalesResult> Handle(GetSalesCommand request, CancellationToken cancellationToken)
    {
        var (sales, totalCount) = await _saleRepository.GetAllAsync(
            request.Page,
            request.Size,
            request.OrderBy,
            request.CustomerId,
            request.CreatedAtMin,
            request.CreatedAtMax,
            request.TotalAmountMin,
            request.TotalAmountMax,
            request.Branch,
            request.Status);

        return new GetSalesResult(sales, request.Page, request.Size, totalCount);
    }
} 