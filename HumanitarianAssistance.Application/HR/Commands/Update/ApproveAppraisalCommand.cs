using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.HR.Commands.Update
{
    public class ApproveAppraisalCommand: BaseModel, IRequest<bool>
    {
        public int EmployeeAppraisalDetailsId { get; set; }
    }
}