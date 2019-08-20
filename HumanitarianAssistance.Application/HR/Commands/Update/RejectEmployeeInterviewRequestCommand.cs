using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.HR.Commands.Update
{
    public class RejectEmployeeInterviewRequestCommand : BaseModel, IRequest<ApiResponse>
    {
        public int InterviewDetailsId { get; set; }
    }
}
