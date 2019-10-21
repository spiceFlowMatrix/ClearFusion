using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Marketing.Queries
{
    public class GetAllPolicyScheduleListQuery:IRequest<ApiResponse>
    {
        public string text { get; set; }
    }
}
