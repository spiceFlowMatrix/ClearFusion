using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetAllEmployeeListQuery : IRequest<ApiResponse>
    {
        public int ProjectId { get; set; }
        public int HiringRequestId { get; set; }
    }
}
