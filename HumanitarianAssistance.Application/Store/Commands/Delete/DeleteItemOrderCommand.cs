using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Store.Commands.Delete
{
    public class DeleteItemOrderCommand:BaseModel,IRequest<ApiResponse>
    {
        public long OrderId { get; set; }
    }
}
