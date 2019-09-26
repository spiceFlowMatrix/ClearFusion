using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class ProjectActivityReportPdfQuery : IRequest<byte[]>
    {
        public string FilterValue { get; set; }           
    }
}
