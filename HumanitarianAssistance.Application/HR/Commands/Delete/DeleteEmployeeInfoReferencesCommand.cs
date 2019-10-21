using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.HR.Commands.Delete
{
   public class DeleteEmployeeInfoReferencesCommand : BaseModel, IRequest<ApiResponse>
    {
        public int EmployeeInfoReferencesId { get; set; }
    }
}
