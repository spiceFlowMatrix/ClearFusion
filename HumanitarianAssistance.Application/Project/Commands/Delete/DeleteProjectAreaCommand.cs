using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Commands.Delete
{
    public class DeleteProjectAreaCommand: BaseModel, IRequest<ApiResponse>
    {
        public long ProjectAreaId { get; set; }
        public long ProjectId { get; set; }
        public long AreaId { get; set; }
    }
}