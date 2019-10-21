using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Commands.Create
{
    public class AddHiringRequestCandidateCommand : BaseModel, IRequest<ApiResponse>
    {
        public long CandidateId { get; set; }
        public long HiringRequestId { get; set; }
        public int? EmployeeID { get; set; }
        public long? ProjectId { get; set; }
    }
}
