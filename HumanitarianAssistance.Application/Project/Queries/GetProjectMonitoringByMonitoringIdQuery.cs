using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetProjectMonitoringByMonitoringIdQuery : IRequest<ApiResponse>
    {
        public int Id { get; set; } 
    }
}
