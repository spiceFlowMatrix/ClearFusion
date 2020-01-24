using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.HR.Commands.Update
{
    public class RevokeEmployeePayrollCommand: BaseModel, IRequest<object>
    {
        public int EmployeeId { get; set; }
        public int Month { get; set; }
    }
}