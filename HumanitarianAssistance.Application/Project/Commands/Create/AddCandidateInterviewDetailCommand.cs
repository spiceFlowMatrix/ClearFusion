using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Commands.Create
{
    public class AddCandidateInterviewDetailCommand : BaseModel, IRequest<ApiResponse>
    {
        public int? InterviewDetailsId { get; set; }
        public int EmployeeID { get; set; }
        public long? JobId { get; set; }
        public string Status { get; set; }
        public string JobDescription { get; set; }
    }
}
