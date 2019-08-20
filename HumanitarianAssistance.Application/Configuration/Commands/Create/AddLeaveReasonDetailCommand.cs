using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Configuration.Commands.Create
{
   public class AddLeaveReasonDetailCommand : BaseModel, IRequest<ApiResponse>
    {
        public int LeaveReasonId { get; set; }
        public string ReasonName { get; set; }
        public int Unit { get; set; }
    }
}
