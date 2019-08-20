using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Commands.Common
{
    public class AddEditProjectProgramCommand: BaseModel, IRequest<ApiResponse>
    {
        public long ProjectProgramId { get; set; }
        public long ProjectId { get; set; }      
        public long ProgramId { get; set; }
    }
}