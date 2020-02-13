using System;
using System.Collections.Generic;
using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.HR.Commands.Create
{
    public class AddPayrollDailyHoursToAttendanceGroupsCommand : BaseModel, IRequest<bool>
    {
        public long AttendanceGroupId { get; set; }
        public DateTime Date { get; set; }
        public DateTime InTime { get; set; }
        public DateTime OutTime { get; set; }
        public int? OfficeId { get; set; }
        public int? Hours { get; set; }
    }
}