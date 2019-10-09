using HumanitarianAssistance.Application.Configuration.Models;
using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetAnnualAppraisalReportPdfQuery : BaseModel, IRequest<byte[]>
    {
        public int OfficeId { get; set; }
    }
}  
