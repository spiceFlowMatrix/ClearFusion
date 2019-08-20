using HumanitarianAssistance.Application.HR.Models;
using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using System.Collections.Generic;

namespace HumanitarianAssistance.Application.HR.Commands.Create
{
    public class AddInterviewScheduleDetailsCommand : BaseModel, IRequest<ApiResponse>
    {
        public List<InterviewScheduleModel> InterViewSchedule { get; set; }  
    }
}
