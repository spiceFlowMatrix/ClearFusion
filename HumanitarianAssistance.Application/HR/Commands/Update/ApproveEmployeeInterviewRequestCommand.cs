using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.HR.Commands.Update
{
    public class ApproveEmployeeInterviewRequestCommand : BaseModel, IRequest<ApiResponse>
    {
        public int InterviewDetailsId { get; set; } 
    }
}
