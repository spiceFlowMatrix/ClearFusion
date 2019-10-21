using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Commands.Common
{
    public class HiringRequestSelectCandidateCommand : BaseModel, IRequest<ApiResponse>
    {
        public int EmployeeId { get; set; }
        public long HiringRequestId { get; set; }
        public long ProjectId { get; set; }
        public long BudgetLineId { get; set; }
    }
}
