using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetTransactionListQuery : IRequest<ApiResponse>
    {
        public string UserName { get; set; }
        public int CurrencyId { get; set; }
        public long BudgetLineId { get; set; }
    }
}
