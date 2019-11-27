using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetInterviewDetailsByInterviewIdQuery: IRequest<ApiResponse>
    {
        public int InterviewId { get; set; }
    }
}