using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Marketing.Commands.Update
{
    public class AcceptAgreementCommand : BaseModel, IRequest<ApiResponse>
    {
        public int JobId { get; set; }
    }
}
