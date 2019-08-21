using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Commands.Create
{
    public class AddProgramDetailCommand: BaseModel, IRequest<ApiResponse>
    {
        public long? ProgramId { get; set; }
        public string ProgramName { get; set; }
        public string ProgramCode { get; set; }
        public long? ProjectId { get; set; }
    }
}