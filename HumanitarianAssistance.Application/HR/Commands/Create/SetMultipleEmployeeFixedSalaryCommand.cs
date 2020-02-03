using System;
using System.Collections.Generic;
using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.HR.Commands.Create
{
    public class SetMultipleEmployeeFixedSalaryCommand : BaseModel, IRequest<object>
    {
        public List<int> EmployeeIds { get; set; }
        public double? FixedSalary { get; set; }
        public double? CapacityBuilding { get; set; }
        public double? Security { get; set; }
    }
}
