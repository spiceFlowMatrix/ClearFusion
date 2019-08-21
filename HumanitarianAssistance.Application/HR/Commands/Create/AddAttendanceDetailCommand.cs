using System;
using System.Collections.Generic;
using HumanitarianAssistance.Application.HR.Models;
using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.HR.Commands.Create
{
    public class AddAttendanceDetailCommand: BaseModel, IRequest<ApiResponse>
    {
        public List<EmployeeAttendanceModel> EmployeeAttendance { get; set; }
    }
}