using System.Threading;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSaleById
{
    public class GetSaleByIdQueryHandler : IRequestHandler<GetSaleByIdQuery, Sale>
    {
        private readonly ISaleRepository _saleRepository;

        public GetSaleByIdQueryHandler(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        public async Task<Sale> Handle(GetSaleByIdQuery request, CancellationToken cancellationToken)
        {
            var sale = await _saleRepository.GetByIdAsync(request.Id);
            if (sale == null)
                throw new KeyNotFoundException($"Sale with id {request.Id} not found");

            return sale;
        }
    }
} 