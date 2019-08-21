using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetBudgetLineDetailByBudgetIdQuery : IRequest<ApiResponse>
    {
        public int BudgetId { get; set; } 
    }
}
