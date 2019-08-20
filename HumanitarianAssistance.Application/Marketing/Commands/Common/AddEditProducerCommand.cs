using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Marketing.Commands.Common
{
    public class AddEditProducerCommand:BaseModel,IRequest<ApiResponse>
    {
        public long? ProducerId { get; set; }
        public string ProducerName { get; set; }
    }
}
