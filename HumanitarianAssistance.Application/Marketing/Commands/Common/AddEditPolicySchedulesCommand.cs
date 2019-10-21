using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Marketing.Commands.Common
{
    public class AddEditPolicySchedulesCommand:BaseModel, IRequest<ApiResponse>
    {
        public long PolicyScheduleId { get; set; }
        public long? PolicyId { get; set; }
        public string Title { get; set; }
        public string[] RepeatDays { get; set; }
        public string Description { get; set; }
        public int? Frequency { get; set; }
        public int? ByMonth { get; set; }
        public int? ByWeek { get; set; }
        public int? ByDay { get; set; }
        public string StartDate { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string EndDate { get; set; }
        public bool isActive { get; set; }
    }
}
