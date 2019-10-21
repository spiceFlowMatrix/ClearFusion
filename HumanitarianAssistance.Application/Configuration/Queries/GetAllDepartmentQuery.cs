using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Configuration.Queries
{
    public class GetAllDepartmentQuery : BaseModel, IRequest<ApiResponse>
    {

    }
}
