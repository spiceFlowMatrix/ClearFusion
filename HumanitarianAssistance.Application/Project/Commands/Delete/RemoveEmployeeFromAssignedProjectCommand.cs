using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Commands.Delete
{
    public class RemoveEmployeeFromAssignedProjectCommand: BaseModel, IRequest<ApiResponse>
    {
        public long ProjectAssignToId { get; set; }
        public long ProjectId { get; set; }
        public int EmployeeId { get; set; }
    }
}