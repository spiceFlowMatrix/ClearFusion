using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.HR.Queries
{
   public class GetAllEmployeeLanguagesQuery : BaseModel, IRequest<ApiResponse>
    {
        public int EmployeeId { get; set; }
    }
}
