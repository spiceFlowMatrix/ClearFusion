using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Marketing.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Marketing.Commands.Common
{
    public class AddEditPolicyRepeatDaysCommand : BaseModel, IRequest<ApiResponse>
    {
        public long? PolicyId { get; set; }
        public List<RepeatDaysModel> RepeatDays { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public long Id { get; set; }
    }
}
