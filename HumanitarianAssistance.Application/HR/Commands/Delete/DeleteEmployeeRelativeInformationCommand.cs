using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.HR.Commands.Delete
{
   public class DeleteEmployeeRelativeInformationCommand : BaseModel, IRequest<ApiResponse>
    {
        public int EmployeeRelativeInfoId { get; set; }
    }
}
