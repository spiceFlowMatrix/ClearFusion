using System.Collections.Generic;
using HumanitarianAssistance.Application.HR.Models;
using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.HR.Commands.Update
{
    public class DeleteEmployeeByEmployeeIdCommand: BaseModel, IRequest<object>
    {
        public int EmployeeId { get; set; }
    }
}