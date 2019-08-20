using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Commands.Delete
{
    public class DeleteAreaDetailCommand: BaseModel, IRequest<ApiResponse>
    {
        public long AreaId { get; set; }
        public string AreaName { get; set; }
        public string AreaCode { get; set; }
    }
}