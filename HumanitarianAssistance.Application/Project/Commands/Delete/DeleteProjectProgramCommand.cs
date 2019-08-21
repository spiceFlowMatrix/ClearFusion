using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Commands.Delete
{
    public class DeleteProjectProgramCommand: BaseModel, IRequest<ApiResponse>
    {
        public long ProjectProgramId  { get; set; }
        public long ProjectId { get; set; }
        public long ProgramId { get; set; }
    }
}