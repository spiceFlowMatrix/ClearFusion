using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Marketing.Commands.Create
{
    public class CreatePDFCommand : BaseModel, IRequest<ApiResponse>
    {
        public int JobId { get; set; }  
    }
}
