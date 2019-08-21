using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Marketing.Commands.Common
{
    public class AddEditPlayoutMinutesCommand:BaseModel,IRequest<ApiResponse>
    {
        public long PlayoutMinuteId { get; set; }
        public long? ScheduleId { get; set; }
        public long? TotalMinutes { get; set; }
        public long? DroppedMinutes { get; set; }
    }
}
