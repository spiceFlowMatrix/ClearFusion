using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Marketing.Queries
{
    public class FilterScheduleListQuery:IRequest<ApiResponse>
    {
        public long? MediumId { get; set; }
        public long? ChannelId { get; set; }
        public DateTime StartDate { get; set; }
    }
}
