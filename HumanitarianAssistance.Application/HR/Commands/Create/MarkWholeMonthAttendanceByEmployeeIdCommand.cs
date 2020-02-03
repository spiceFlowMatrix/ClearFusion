using System.Collections.Generic;
using HumanitarianAssistance.Application.HR.Models;
using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.HR.Commands.Create
{
    public class MarkWholeMonthAttendanceByEmployeeIdCommand: BaseModel, IRequest<object>
    {
        public int EmployeeId { get; set; }
        public int Month { get; set; }  
        public int Year { get; set; }
    }
}