using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Commands.Common
{
    public class AddEditProjectSectorCommand: BaseModel, IRequest<ApiResponse>
    {
        public long ProjectSectorId { get; set; }
        public long ProjectId { get; set; }       
        public long SectorId { get; set; }
    }
}