using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.HR.Commands.Update
{
  public  class UpdateEmployeeImageCommand : BaseModel, IRequest<ApiResponse>
    {
        public int EmployeeId { get; set; }
        public string EmployeeImage { get; set; }
    }
}
