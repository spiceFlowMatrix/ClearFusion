using System.Collections.Generic;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Marketing.Models;
using MediatR;

namespace HumanitarianAssistance.Application.HR.Commands.Update
{
    public class ApproveEmployeeLeaveCommand: BaseModel, IRequest<ApiResponse>
    {
        public List<ApproveLeaveModel> AppliedLeave { get; set;}
    }
}