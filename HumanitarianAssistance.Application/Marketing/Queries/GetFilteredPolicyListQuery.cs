using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Marketing.Queries
{
    public class GetFilteredPolicyListQuery: IRequest<ApiResponse>
    {
        public bool Medium { get; set; }
        public string Value { get; set; }
        public bool PolicyId { get; set; }
        public bool PolicyName { get; set; }
    }
}
