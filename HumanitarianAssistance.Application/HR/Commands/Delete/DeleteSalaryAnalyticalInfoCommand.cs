using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.HR.Commands.Delete
{
   public class DeleteSalaryAnalyticalInfoCommand : BaseModel, IRequest<ApiResponse>
    {
        public int EmployeeSalaryAnalyticalInfoId { get; set; }

    }
}
