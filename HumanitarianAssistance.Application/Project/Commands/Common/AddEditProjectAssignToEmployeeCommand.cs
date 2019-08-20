using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Commands.Common
{
    public class AddEditProjectAssignToEmployeeCommand: BaseModel, IRequest<ApiResponse>
    {
        public long ProjectAssignToId { get; set; }
        public long ProjectId { get; set; }
        public int EmployeeId { get; set; }
    }
}