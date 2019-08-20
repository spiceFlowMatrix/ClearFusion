using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetActivitiesControlPermissionQuery : IRequest<ApiResponse>
    {
        public long projectId { get; set; }
        public string userId { get; set; }  
    }
}
