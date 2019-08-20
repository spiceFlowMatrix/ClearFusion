using System.Collections.Generic;
using HumanitarianAssistance.Application.HR.Models;
using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.HR.Commands.Create
{
    public class AddEmployeeLeaveCommand: BaseModel, IRequest<ApiResponse>
    {
        public List<AssignLeaveToEmployeeModel> AssignEmployeeLeaveList { get; set; }
    }
}