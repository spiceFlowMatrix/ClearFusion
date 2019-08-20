using HumanitarianAssistance.Application.HR.Models;
using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using System.Collections.Generic;

namespace HumanitarianAssistance.Application.HR.Commands.Update
{
    public class InterviewApprovalsCommand : BaseModel, IRequest<ApiResponse>
    {
        public List<InterviewScheduleModel> InterViewSchedule { get; set; }
        public int approvalId { get; set; } 
    }
}
