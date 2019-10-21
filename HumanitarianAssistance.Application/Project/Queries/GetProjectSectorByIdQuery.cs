using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetProjectSectorByIdQuery: IRequest<ApiResponse>
    {
        public long ProjectId { get; set; }
    }
}