using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Commands.Update
{
    public class EditProgramDetailCommand: BaseModel, IRequest<ApiResponse>
    {
        public long? ProgramId { get; set; }
        public string ProgramName { get; set; }
        public string ProgramCode { get; set; }
        public long? ProjectId { get; set; }
    }
}