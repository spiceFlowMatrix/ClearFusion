using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Marketing.Commands.Delete
{
    public class DeletePolicyTimeScheduleCommand:BaseModel,IRequest<ApiResponse>
    {
        public int Id { get; set; }
    }
}
