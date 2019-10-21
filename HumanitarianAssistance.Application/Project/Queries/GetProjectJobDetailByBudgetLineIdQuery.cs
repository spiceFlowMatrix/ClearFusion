using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetProjectJobDetailByBudgetLineIdQuery : IRequest<ApiResponse>
    {
        public long BudgetLineId { get; set; }  
    }
}
