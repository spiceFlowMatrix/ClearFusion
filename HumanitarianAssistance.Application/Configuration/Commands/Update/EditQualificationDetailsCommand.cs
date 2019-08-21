using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Configuration.Commands.Update
{
  public  class EditQualificationDetailsCommand : BaseModel, IRequest<ApiResponse>
    {
        public int QualificationId { get; set; }
        public string QualificationName { get; set; }
    }
}
