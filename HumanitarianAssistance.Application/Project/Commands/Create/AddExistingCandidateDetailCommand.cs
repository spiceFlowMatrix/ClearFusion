using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Commands.Create {
    public class AddExistingCandidateDetailCommand : BaseModel, IRequest<ApiResponse> {
        public long ProjectId { get; set; }
        public long HiringRequestId { get; set; }
        public int EmployeeId { get; set; }
    }
}