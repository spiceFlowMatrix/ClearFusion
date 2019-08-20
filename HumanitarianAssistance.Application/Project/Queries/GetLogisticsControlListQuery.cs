using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetLogisticsControlListQuery : IRequest<ApiResponse>
    {
        public long projectId { get; set; }
    }
}
