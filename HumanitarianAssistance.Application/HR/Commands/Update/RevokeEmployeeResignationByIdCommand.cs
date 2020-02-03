using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.HR.Commands.Update
{
    public class RevokeEmployeeResignationByIdCommand: BaseModel, IRequest<object>
    {
        public int EmployeeId { get; set; }
    }
}