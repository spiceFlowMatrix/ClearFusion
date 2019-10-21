using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class AllProjectActivityStatusQuery : IRequest<ApiResponse>
    {
        public long ProjectId { get; set; }
    }
}
