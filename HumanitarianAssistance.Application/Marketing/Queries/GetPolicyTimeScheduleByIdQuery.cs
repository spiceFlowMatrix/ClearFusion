using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Marketing.Queries
{
    public class GetPolicyTimeScheduleByIdQuery:IRequest<ApiResponse>
    {
        public int Id { get; set; }
    }
}
