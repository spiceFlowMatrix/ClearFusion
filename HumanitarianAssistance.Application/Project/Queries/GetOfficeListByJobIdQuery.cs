using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetOfficeListByJobIdQuery : IRequest<ApiResponse>
    {
        public long ProjectId { get; set; } 
        public int ProfessionId { get; set; } 
    }
}
