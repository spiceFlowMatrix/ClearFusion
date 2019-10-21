using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Configuration.Commands.Common
{
    public class ApproveEmployeeAppraisalRequestCommand : BaseModel, IRequest<ApiResponse>
    {
        public int EmployeeAppraisalDetailsId { get; set; }
    }
}
