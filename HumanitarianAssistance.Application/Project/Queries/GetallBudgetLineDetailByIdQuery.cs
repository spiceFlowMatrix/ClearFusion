using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetallBudgetLineDetailByIdQuery : IRequest<ApiResponse>
    {
        public long ProjectId { get; set; }
    }
}
