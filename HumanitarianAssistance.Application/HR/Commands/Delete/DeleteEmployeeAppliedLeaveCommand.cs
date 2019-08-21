using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.HR.Commands.Delete
{
    public class DeleteEmployeeAppliedLeaveCommand: BaseModel, IRequest<ApiResponse>
    {
        public long AppliedLeaveId { get; set; }
    }
}