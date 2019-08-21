using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Configuration.Commands.Create
{
   public class AddQualificationDetailsCommand : BaseModel, IRequest<ApiResponse>
    {
        public int QualificationId { get; set; }
        public string QualificationName { get; set; }
    }
}
