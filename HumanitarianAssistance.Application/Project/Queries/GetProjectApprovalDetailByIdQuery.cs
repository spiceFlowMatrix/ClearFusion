using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetProjectApprovalDetailByIdQuery: IRequest<ApiResponse>
    {
        public long ProjectId { get; set; }
    }
}