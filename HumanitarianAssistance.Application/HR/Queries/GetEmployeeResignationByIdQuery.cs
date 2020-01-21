using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetEmployeeResignationByIdQuery : BaseModel, IRequest<object>
    {
        public int EmployeeID { get; set; }

    }
}
