using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetProjectMonitoringListQuery : IRequest<ApiResponse>
    {
        public long activityId { get; set; }  
    }
}
