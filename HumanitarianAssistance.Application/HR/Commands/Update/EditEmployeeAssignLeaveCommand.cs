using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.HR.Commands.Update
{
    public class EditEmployeeAssignLeaveCommand: BaseModel, IRequest<ApiResponse>
    {
        public long LeaveId { get; set; }
        public int? AssignUnit { get; set; }
    }
}