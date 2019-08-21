using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Marketing.Commands.Delete
{
    public class DeletePolicyCommand:BaseModel, IRequest<ApiResponse>
    {
        public int PolicyId { get; set; }
    }
}
