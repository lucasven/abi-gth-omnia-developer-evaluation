using Ambev.DeveloperEvaluation.Domain.Entities;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSaleById
{
    public class GetSaleByIdQuery : IRequest<Sale>
    {
        public Guid Id { get; set; }
    }
} 