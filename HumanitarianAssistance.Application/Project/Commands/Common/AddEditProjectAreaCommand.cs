using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Commands.Common
{
    public class AddEditProjectAreaCommand: BaseModel, IRequest<ApiResponse>
    {
        public long ProjectAreaId { get; set; }
        public long ProjectId { get; set; }
        public long AreaId { get; set; }
    }
}