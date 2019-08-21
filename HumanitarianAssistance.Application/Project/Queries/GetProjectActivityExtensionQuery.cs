using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetProjectActivityExtensionQuery : IRequest<ApiResponse>
    {
        public long activityId { get; set; }
    }
}
