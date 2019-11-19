using System;
using System.Collections.Generic;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Project.Models;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetCandidateDetailReportPdfQuery  : BaseModel, IRequest<byte[]>
    {    
         public long HiringRequestId { get; set; }
         public long ProjectId { get; set; }
    }
}