using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetOfficeListByJobIdQuery : IRequest<ApiResponse>
    {
        public int JobId { get; set; } 
    }
}
