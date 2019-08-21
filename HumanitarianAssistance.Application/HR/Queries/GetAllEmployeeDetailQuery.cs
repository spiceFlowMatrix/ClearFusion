using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.HR.Queries
{
 public  class GetAllEmployeeDetailQuery: BaseModel, IRequest<ApiResponse>
    {
        public int  EmployeeType { get; set; }
        public int OfficeId { get; set; }
    }
}
