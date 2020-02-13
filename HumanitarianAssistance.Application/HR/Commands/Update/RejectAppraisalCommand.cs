using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.HR.Commands.Update
{
    public class RejectAppraisalCommand: BaseModel, IRequest<bool>
    {
        public int EmployeeAppraisalDetailsId { get; set; }
    }
}