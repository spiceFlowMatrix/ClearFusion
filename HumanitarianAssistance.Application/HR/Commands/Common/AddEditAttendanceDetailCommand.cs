using System;
using System.Collections.Generic;
using HumanitarianAssistance.Application.HR.Models;
using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.HR.Commands.Common
{
    public class AddEditAttendanceDetailCommand: BaseModel, IRequest<object>
    {
        public List<int> OfficeIds { get; set; }
        public DateTime AttendanceDate { get; set; }
        public List<EmployeeAttendanceModel> EmployeeAttendance { get; set; }
    }
}