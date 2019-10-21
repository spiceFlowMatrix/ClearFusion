using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Marketing.Commands.Common
{
    public class AddEditChannelCommand:BaseModel,IRequest<ApiResponse>
    {
        public long ChannelId { get; set; }
        public string ChannelName { get; set; }
        public long? MediumId { get; set; }
        public string MediumName { get; set; }
    }
}
