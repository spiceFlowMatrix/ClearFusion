using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Commands.Delete
{
    public class DeleteProjectSectorCommand : BaseModel, IRequest<ApiResponse>
    {
        public long ProjectSectorId { get; set; }
        public long ProjectId { get; set; }
        public long SectorId { get; set; }
    }
}
