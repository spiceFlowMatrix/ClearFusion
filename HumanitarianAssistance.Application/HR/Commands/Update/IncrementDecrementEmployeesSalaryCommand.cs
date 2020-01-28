using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.HR.Commands.Update
{
   public class IncrementDecrementEmployeesSalaryCommand : BaseModel, IRequest<object>
    {
        public List<int> EmployeeIds { get; set; }
        public float? Percentage { get; set; }
        public double? Amount { get; set; }
        public int ReconfigureType { get; set; }
    }
}
