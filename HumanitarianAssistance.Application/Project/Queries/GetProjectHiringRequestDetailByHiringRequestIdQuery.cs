using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetProjectHiringRequestDetailByHiringRequestIdQuery : IRequest<ApiResponse>
    {
        public long HiringRequestId { get; set; }
    }
}