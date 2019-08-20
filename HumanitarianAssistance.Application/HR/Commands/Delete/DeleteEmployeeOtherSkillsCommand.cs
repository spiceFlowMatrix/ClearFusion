using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.HR.Commands.Delete
{
  public  class DeleteEmployeeOtherSkillsCommand : BaseModel, IRequest<ApiResponse>
    {
        public int EmployeeOtherSkillsId { get; set; }
    }
}
