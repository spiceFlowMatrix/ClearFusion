using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetAllProjectJobByProjectIdQuery : IRequest<ApiResponse>
    {
        public long ProjectJobId { get; set; } 
    }
} 
