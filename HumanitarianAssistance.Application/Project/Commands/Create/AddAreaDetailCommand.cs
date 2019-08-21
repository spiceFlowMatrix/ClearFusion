using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Commands.Create
{
    public class AddAreaDetailCommand : BaseModel, IRequest<ApiResponse>
    {
        public long AreaId { get; set; }
        public string AreaName { get; set; }
        public string AreaCode { get; set; }
    }
}