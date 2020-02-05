using System;
using System.Collections.Generic;
using HumanitarianAssistance.Application.HR.Models;
using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.HR.Commands.Update
{
    public class TerminateEmployeeByEmployeeIdCommand: BaseModel, IRequest<object>
    {
        public int EmployeeId { get; set; }                                                                                                                  
        public DateTime TerminationDate { get; set; }
        public string ReasonOfTermination { get; set; }
    }
}