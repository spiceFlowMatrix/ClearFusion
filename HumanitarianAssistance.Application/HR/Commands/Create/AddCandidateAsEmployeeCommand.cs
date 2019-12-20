using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.HR.Commands.Create
{
    public class AddCandidateAsEmployeeCommand: BaseModel, IRequest<ApiResponse>
    {
        public long CandidateId { get; set; }
        public long HiringRequestId { get; set; }
        public long ProjectId { get; set; }
        public int StatusId { get; set; }
    }
}