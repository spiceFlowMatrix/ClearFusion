using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Marketing.Commands.Delete
{
    public class DeleteScheduleCommand:BaseModel,IRequest<ApiResponse>
    {
        public int model { get; set; }
    }
}
