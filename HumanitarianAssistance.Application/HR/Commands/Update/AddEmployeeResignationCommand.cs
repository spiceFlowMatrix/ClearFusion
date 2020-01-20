using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.HR.Commands.Update
{
    public class AddEmployeeResignationCommand : BaseModel, IRequest<bool>
    {
        public int EmployeeID { get; set; } 
    }
}
