using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetAllHiringRequestDetailForInterviewByHiringRequestIdQuery : IRequest<ApiResponse>
    {
         public long ProjectId { get; set; }
         public long HiringRequestId { get; set; }
    }
}