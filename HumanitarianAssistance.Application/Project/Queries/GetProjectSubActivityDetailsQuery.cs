using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetProjectSubActivityDetailsQuery : IRequest<ApiResponse>
    {
        public int projectId { get; set; } 
    }
}
