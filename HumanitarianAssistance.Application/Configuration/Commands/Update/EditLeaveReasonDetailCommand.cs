using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Configuration.Commands.Update
{
   public class EditLeaveReasonDetailCommand : BaseModel, IRequest<ApiResponse>
    {
        public int LeaveReasonId { get; set; }
        public string ReasonName { get; set; }
        public int Unit { get; set; }
    }
}
